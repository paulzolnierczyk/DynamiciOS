using System.Collections.Generic;
using Newtonsoft.Json;

namespace LIT_Storyboard
{
    public class DynamicScreens
    {
        [JsonProperty("screens")]
        public List<DynamicScreen> Screens { get; set; }
    }
}
