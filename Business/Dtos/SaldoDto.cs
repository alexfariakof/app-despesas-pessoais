using Business.HyperMedia;
using Business.HyperMedia.Abstractions;

namespace Business.Dtos;
public class SaldoDto: ISupportHyperMedia
{
    public decimal saldo { get; set; }
    public static implicit operator decimal(SaldoDto d) => d.saldo;
    public static implicit operator SaldoDto(decimal saldo) => new SaldoDto(saldo);
    public SaldoDto(decimal saldo)
    {
        this.saldo = saldo;
    }   

    public IList<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
}