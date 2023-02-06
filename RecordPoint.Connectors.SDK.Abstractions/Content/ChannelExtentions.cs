using Newtonsoft.Json;

namespace RecordPoint.Connectors.SDK.Content
{
    public static class ChannelExtentions
    {
        public static Channel ToChannel(this ChannelModel channelModel)
        {
            if (channelModel == null)
            {
                throw new ArgumentNullException(nameof(channelModel));
            }
            if (string.IsNullOrEmpty(channelModel.Title))
            {
                throw new RequiredValueNullException(nameof(channelModel.Title));
            }

            List<MetaDataItem> items;
            if (string.IsNullOrEmpty(channelModel.MetaData))
            {
                items = new();
            }
            else
            {
                var deserialisedItems = JsonConvert.DeserializeObject<List<MetaDataItem>>(channelModel.MetaData);
                if (deserialisedItems == null)
                {
                    items = new();
                }
                else
                {
                    items = deserialisedItems;
                }
            }

            return new()
            {
                ExternalId = channelModel.ExternalId,
                Title = channelModel.Title,
                MetaDataItems = items
            };
        }

        public static List<Channel> ToChannelList(this IEnumerable<ChannelModel> channelModels) => channelModels.Select(a => a.ToChannel()).ToList();


        public static ChannelModel ToChannelModel(this Channel channel)
        {
            return new()
            {
                ExternalId = channel.ExternalId,
                Title = channel.Title,
                MetaData = JsonConvert.SerializeObject(channel.MetaDataItems)
            };
        }

        public static List<ChannelModel> ToChannelModelList(this IEnumerable<Channel> channels) => channels.Select(a => a.ToChannelModel()).ToList();
    }
}
