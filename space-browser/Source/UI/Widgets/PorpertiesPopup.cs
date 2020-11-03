using System;
using System.Collections.Generic;
using System.Text;

namespace space_browser.Source.UI.Widgets
{
    public class PorpertiesPopup : Popup
    {
        public override IPayload IPayload { get => payload; }
        private Payload payload;

        public PorpertiesPopup(Payload payload) : base()
        {
            this.payload = payload;
        }

        public class Payload : IPayload
        {
            public string Content { get; set; }
            public Payload(string text)
            {
                Content = text;
            }
        }
    }
}
