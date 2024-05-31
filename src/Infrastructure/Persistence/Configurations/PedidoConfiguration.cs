using dotnet_incahuasi_worker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_incahuasi_worker.Infrastructure.Persistence.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("pedidos");
        builder.Property(e => e.id);     
        builder.Property(e => e.numeroDePedido);
        builder.Property(e => e.cicloDelPedido);
        builder.Property(e => e.codigoDeContratoInterno);
        builder.Property(e => e.estadoDelPedido);
        builder.Property(e => e.cuentaCorriente);
        builder.Property(e => e.cuando);
    }
}
