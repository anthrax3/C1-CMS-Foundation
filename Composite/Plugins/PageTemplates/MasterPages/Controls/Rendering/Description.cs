﻿using System.Web.UI;

using Composite.Core.WebClient.Renderings.Page;

namespace Composite.Plugins.PageTemplates.MasterPages.Controls.Rendering
{
    /// <exclude />
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)] 
    public class Description : Control
    {
        /// <exclude />
        protected override void Render(HtmlTextWriter writer)
        {
            string description = PageRenderer.CurrentPage.Description;
            if(string.IsNullOrEmpty(description)) return;

            writer.WriteEncodedText(description);
        }
    }
}
