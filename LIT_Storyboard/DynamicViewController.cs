using System;
using Foundation;
using Newtonsoft.Json;
using UIKit;

namespace LIT_Storyboard
{
    public partial class DynamicViewController : UIViewController
    {

        UIScrollView scrollView;
        UIStackView stackView;

        nfloat frameWidth;
        nfloat frameHeight;
        nfloat elementWidthFull;
        nfloat gutterSpacing = 16;

        DynamicScreens DynamicScreens;
        int ScreenToDraw;

        public DynamicViewController(int screenToDraw) 
        {
            ScreenToDraw = screenToDraw;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            frameWidth = 360; //View.Frame.Width;
            frameHeight = View.Frame.Height;
            elementWidthFull = frameWidth - (gutterSpacing * 2);

            //Parent Scroll View
            scrollView = new UIScrollView(View.Frame);
            scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            View.AddSubview(scrollView);

            //Child stack view that will have scroll view as parent
            stackView = new UIStackView(scrollView.Frame);
            stackView.TranslatesAutoresizingMaskIntoConstraints = false;
            stackView.Axis = UILayoutConstraintAxis.Vertical;
            stackView.Alignment = UIStackViewAlignment.Center;
            stackView.LayoutMargins = new UIEdgeInsets(top: 0, left: 0, bottom: 20, right: 0);
            stackView.LayoutMarginsRelativeArrangement = true;
            scrollView.AddSubview(stackView);

            //Need to add constraints in code
            AddConstraintToView(View, scrollView, "scrollView", "H:|[scrollView]|", NSLayoutFormatOptions.AlignAllCenterX);
            AddConstraintToView(View, scrollView, "scrollView", "V:|[scrollView]|", NSLayoutFormatOptions.AlignAllCenterX);

            AddConstraintToView(scrollView, stackView, "stackView", "H:|[stackView]|", NSLayoutFormatOptions.AlignAllCenterX);
            AddConstraintToView(scrollView, stackView, "stackView", "V:|[stackView]|", NSLayoutFormatOptions.AlignAllCenterX);

            //Right now it's hard coded JSON, but this is where it would come from an API
            DynamicScreens = JsonConvert.DeserializeObject<DynamicScreens>(DynamicScreenJSON.json);

            var screens = DynamicScreens?.Screens;

            if (screens != null && screens.Count > 0)
            {
                var screen = DynamicScreens.Screens[ScreenToDraw];

                foreach (var element in screen.Elements)
                {
                    var name = element.ElementName;

                    var view = RenderView(element);

                    if (view != null)
                    {
                        stackView.AddArrangedSubview(view);
                    }
                }
            }

            AddNavigationButtons();
        }

        //For adding a constraints between a single parent view and child view
        public void AddConstraintToView(UIView parentView, UIView childView, string childViewName, string constraint, NSLayoutFormatOptions formatOptions)
        {
            var viewDictionary = NSDictionary.FromObjectsAndKeys(new NSObject[] { childView }, new NSObject[] { new NSString(childViewName) });
            parentView.AddConstraints(NSLayoutConstraint.FromVisualFormat(constraint, formatOptions, new NSDictionary(), viewDictionary));
        }

        public UIStackView GetStackView()
        {
            var view = new UIStackView();
            view.Axis = UILayoutConstraintAxis.Vertical;
            view.LayoutMargins = new UIEdgeInsets(top: 0, left: 0, bottom: 0, right: 0);
            view.LayoutMarginsRelativeArrangement = true;

            return view;
        }

        public UIView RenderView(ElementItem elementItem)
        {
            var name = elementItem.ElementName;

            if(name.Equals("label")){
                var view = GetStackView();

                var label = new UILabel();
                label.Text = elementItem.ElementContent;
                label.LineBreakMode = UILineBreakMode.WordWrap;
                label.Lines = 0;
                label.WidthAnchor.ConstraintEqualTo(elementWidthFull).Active = true;

                view.AddArrangedSubview(label);

                return view;
            }

            if (name.Equals("webView"))
            {
                var view = GetStackView();

                view.AddArrangedSubview(GetWebView(elementItem));

                return view;
            }

            if(name.Equals("button")){
                var view = GetStackView();

                var button = new UIButton();
                button.SetTitle(elementItem.ElementContent, UIControlState.Normal);
                button.SetTitleColor(UIColor.Black, UIControlState.Normal);
                button.WidthAnchor.ConstraintEqualTo(100).Active = true;
                view.AddArrangedSubview(button);
                return view;
            }

            return null;
            
        }

        UIWebView GetWebView(ElementItem elementItem){
            var view = GetStackView();

            var webView = new UIWebView();
            webView.ScrollView.ContentInset = UIEdgeInsets.Zero;
            webView.WidthAnchor.ConstraintEqualTo(elementWidthFull).Active = true;
            webView.HeightAnchor.ConstraintEqualTo(300).Active = true;
            webView.LoadRequest(new NSUrlRequest(new NSUrl(elementItem.ElementContent)));
            webView.ScalesPageToFit = false;

            return webView;
        }

        public void AddNavigationButtons()
        {
            var buttonHeight = 60;

            var navButtonsView = new UIStackView();
            navButtonsView.Axis = UILayoutConstraintAxis.Horizontal;
            navButtonsView.LayoutMargins = new UIEdgeInsets(top: 25, left: gutterSpacing, bottom: 0, right: gutterSpacing);
            navButtonsView.LayoutMarginsRelativeArrangement = true;
            navButtonsView.Spacing = 16;
            navButtonsView.Distribution = UIStackViewDistribution.FillEqually;
            navButtonsView.WidthAnchor.ConstraintEqualTo(frameWidth).Active = true;
            navButtonsView.HeightAnchor.ConstraintEqualTo(buttonHeight + 25).Active = true;

            var button = new UIButton();
            button.SetTitle("Next", UIControlState.Normal);
            button.SetTitleColor(UIColor.Black, UIControlState.Normal);

            button.HeightAnchor.ConstraintEqualTo(buttonHeight).Active = true;
            navButtonsView.AddArrangedSubview(button);

            button.TouchUpInside += Button_TouchUpInside;


            stackView.AddArrangedSubview(navButtonsView);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void Button_TouchUpInside(object sender, EventArgs e)
        {
            if(ScreenToDraw + 1 < DynamicScreens.Screens.Count){
                var dvc = new DynamicViewController(ScreenToDraw + 1);
                PresentViewController(dvc, true, null);
            }
        }
    }
}

