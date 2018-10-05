
namespace LIT_Storyboard
{
    public static class DynamicScreenJSON
    {
        public static string json = @"
{ 'screens': [
    {
        'name': 'Screen 1',
        'elements': [
            {
                'elementName': 'label',
                'elementContent': 'Terms of Use Agreement'
            },
            {
                'elementName': 'webView',
                'elementContent': 'https://www.google.com'
            },
            {
                'elementName': 'label',
                'elementContent': 'You must read through the Terms of Use Agreement in order to continue.'
            },
            {
                'elementName': 'button',
                'elementContent': 'I Accept'
            }
        ]
    },
    {
        'name': 'Screen 2',
        'elements': [
            {
                'elementName': 'webView',
                'elementContent': 'https://www.microsoft.com/'
            },
            {
                'elementName': 'label',
                'elementContent': 'Terms of Use Agreement'
            },
            {
                'elementName': 'button',
                'elementContent': 'I Accept'
            },
            {
                'elementName': 'label',
                'elementContent': 'You must read through the Terms of Use Agreement in order to continue.'
            }
        ]
    }
] 
}";

    }
}
