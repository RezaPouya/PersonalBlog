using PersonalBlog.Utilities.Extensions;

namespace Abin.Website.Domain.Entities.SEOs;

public class Slug : EntityBase
{
    public const string Redirect = "redirect";
    public const string Landing = "landing";
    public const string Project = "project";
    public const string Category = "category";
    public const string Course = "course";
    public const string Article = "article";
    public const string Author = "author";

    public Slug() { }

    public static Slug CreateRedirection(string url, string redirectUrl)
    {
        return new Slug()
        {
            EntityId = 0,
            EntityType = Slug.Redirect,
            Url = url.StringNormalization(toLower: true),
            HasRedirectUrl = true,

        };
    }

    public static Slug Create(string url, int entityId, string entityType)
    {
        return new Slug()
        {
            EntityId = entityId,
            EntityType = entityType,
            Url = url.StringNormalization(toLower: true),
        };
    }

    public static Slug SyncRedirectCreate(Slug seoUrl, string sEOUrl)
    {
        return new Slug()
        {
            EntityId = seoUrl.EntityId,
            EntityType = seoUrl.EntityType,
            Url = sEOUrl.StringNormalization(toLower: true),
            HasRedirectUrl = true,
            RedirectUrl = seoUrl.Url,

        };
    }

    public int EntityId { get; set; }
    public string EntityType { get; set; }
    public string Url { get; set; }
    public string TinyUrl { get; set; }
    public bool HasRedirectUrl { get; set; }
    public string RedirectUrl { get; set; }

    public void UpdateUrl(string url)
    {
        Url = url.StringNormalization(toLower: true);
    }

    public void SetRedirectUrl(string url)
    {
        this.HasRedirectUrl = true;
        this.RedirectUrl = url.StringNormalization(toLower: true);
    }

    public void RemoveRedirectUrl()
    {
        this.HasRedirectUrl = false;
        this.RedirectUrl = null;
    }
}