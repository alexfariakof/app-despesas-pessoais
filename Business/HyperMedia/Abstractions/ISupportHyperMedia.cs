namespace Business.HyperMedia.Abstractions;

public interface ISupportHyperMedia
{
    IList<HyperMediaLink> Links { get; set; }
}
