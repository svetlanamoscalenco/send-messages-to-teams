using Microsoft.Graph;

namespace TeamsMessages
{
    public class GraphApiClientUI
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly IConfiguration _configuration;
        private readonly string? TeamsId;
        private readonly string? ChannelId;

        public GraphApiClientUI(GraphServiceClient graphServiceClient, IConfiguration configuration)
        {
            _graphServiceClient = graphServiceClient;
            _configuration = configuration;
            TeamsId = _configuration.GetValue<string>("Teams:Id");
            ChannelId = _configuration.GetValue<string>("Teams:ChannelId");
        }

        public async Task<User> GetGraphApiUser()
        {
            return await _graphServiceClient
                .Me
                .Request()
                .GetAsync();
        } 
        
        public async Task<ChatMessage> SendMessageToTeamsChannel(string? message)
        {            
            var requestBody = new ChatMessage
            {
                Body = new ItemBody
                {
                    Content = message,
                },
            };
         
            var chatMessage = await _graphServiceClient.Teams[TeamsId]
               .Channels[ChannelId].Messages.Request().AddAsync(requestBody);

            return chatMessage;
        }
                
        public string GetChannelDisplayName()
        {
            var channels = _graphServiceClient.Teams[TeamsId]
                .Channels
                .Request()
                .GetAsync().Result;
             var channel= channels.FirstOrDefault(x => x.Id == ChannelId);
            var channelDisplayName = $"{channel?.Description}/{channel?.DisplayName}";

            return channelDisplayName;
        }
    }
}
