using Newtonsoft.Json;

namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// The channel extentions.
    /// </summary>
    public static class ChannelExtentions
    {
        /// <summary>
        /// Converts to the channel.
        /// </summary>
        /// <param name="channelModel">The channel model.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="RequiredValueNullException"></exception>
        /// <returns>A Channel</returns>
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

        /// <summary>
        /// Converts to channel list.
        /// </summary>
        /// <param name="channelModels">The channel models.</param>
        /// <returns><![CDATA[List<Channel>]]></returns>
        public static List<Channel> ToChannelList(this IEnumerable<ChannelModel> channelModels) => channelModels.Select(a => a.ToChannel()).ToList();


        /// <summary>
        /// Converts to channel model.
        /// </summary>
        /// <param name="channel">The channel.</param>
        /// <returns>A ChannelModel</returns>
        public static ChannelModel ToChannelModel(this Channel channel)
        {
            return new()
            {
                ExternalId = channel.ExternalId,
                Title = channel.Title,
                MetaData = JsonConvert.SerializeObject(channel.MetaDataItems)
            };
        }

        /// <summary>
        /// Converts to channel model list.
        /// </summary>
        /// <param name="channels">The channels.</param>
        /// <returns><![CDATA[List<ChannelModel>]]></returns>
        public static List<ChannelModel> ToChannelModelList(this IEnumerable<Channel> channels) => channels.Select(a => a.ToChannelModel()).ToList();
    }
}
