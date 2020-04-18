using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kurkku.Storage.Database.Data
{
    class CataloguePackageMapping : ClassMap<CataloguePackageData>
    {
        public CataloguePackageMapping()
        {
            Table("catalogue_packages");
            Id(x => x.Id, "id");
            Map(x => x.SaleCode, "salecode");
            Map(x => x.DefinitionId, "definition_id");
            Map(x => x.SpecialSpriteId, "special_sprite_id");
            Map(x => x.Amount, "amount");
        }
    }

    public class CataloguePackageData
    {
        public virtual int Id { get; set; }
        public virtual string SaleCode { get; set; }
        public virtual int DefinitionId { get; set; }
        public virtual int SpecialSpriteId { get; set; }
        public virtual int Amount { get; set; }
    }
}
