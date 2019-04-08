﻿
using System.Data.Entity.ModelConfiguration;
using Sisvon.Model.Entities;

namespace Sisvon.Infra.Data.EntityConfig
{
    public class GrupoConfiguration: EntityTypeConfiguration<Grupo>
    {
        public GrupoConfiguration()
        {
            HasKey(g => g.GrupoId);

        }
    }
}
