using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Data
{
    class CataloguePageMapping : ClassMap<CataloguePageData>
    {
        public CataloguePageMapping()
        {
            Table("catalogue_pages");
            Id(x => x.Id, "id");
            Map(x => x.ParentId, "parent_id");
            Map(x => x.Caption, "caption");
            Map(x => x.MinRank, "min_rank");
            Map(x => x.IconColour, "icon_colour");
            Map(x => x.IconImage, "icon_image");
            Map(x => x.IsNavigatable, "is_navigatable");
            Map(x => x.IsEnabled, "is_enabled");
            Map(x => x.IsClubOnly, "is_club_only");
            Map(x => x.PageLayout, "page_layout");
            Map(x => x.PageHeadline, "page_headline");
            Map(x => x.PageTeaser, "page_teaser");
            Map(x => x.PageSpecial, "page_special");
            Map(x => x.PageText1, "page_text1");
            Map(x => x.PageText2, "page_text2");
            Map(x => x.PageTextDetails, "page_text_details");
            Map(x => x.PageTextTeaser, "page_text_teaser");
            Map(x => x.PageLinkDescription, "page_link_description");
            Map(x => x.PageLinkPageName, "page_link_pagename");
        }
    }

    public class CataloguePageData
    {
        public virtual int Id { get; set; }
        public virtual int ParentId { get; set; }
        public virtual string Caption { get; set; }
        public virtual int MinRank { get; set; }
        public virtual int IconColour { get; set; }
        public virtual int IconImage { get; set; }
        public virtual bool IsVisible { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual bool IsNavigatable { get; set; }
        public virtual bool IsClubOnly { get; set; }
        public virtual string PageLayout { get; set; }
        public virtual string PageHeadline { get; set; }
        public virtual string PageTeaser { get; set; }
        public virtual string PageSpecial { get; set; }
        public virtual string PageText1 { get; set; }
        public virtual string PageText2 { get; set; }
        public virtual string PageTextDetails { get; set; }
        public virtual string PageTextTeaser { get; set; }
        public virtual string PageLinkDescription { get; set; }
        public virtual string PageLinkPageName { get; set; }
    }
}
