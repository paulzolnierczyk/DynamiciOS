using Newtonsoft.Json;

namespace LIT_Storyboard
{
    public class ElementItem
    {
        [JsonProperty("elementName")]
        public string ElementName { get; set; } //Label, Button, RadioButton, DateField
        [JsonProperty("elementContent")]
        public string ElementContent { get; set; } //Titlelabel example = "Hello", TitleLabel2 = "Hello 2", if it's a webview then the HTML etc.
    }
}
