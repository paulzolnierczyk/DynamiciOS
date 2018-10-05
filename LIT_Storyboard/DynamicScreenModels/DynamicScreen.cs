using System.Collections.Generic;
using Newtonsoft.Json;

namespace LIT_Storyboard
{
    public class DynamicScreen
    {
        [JsonProperty("name")]
        public string ScreenName { get; set; }
        [JsonProperty("elements")]
        public List<ElementItem> Elements;
    }
}
