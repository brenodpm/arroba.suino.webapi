using System.Collections.Generic;
using System.Linq;
using arroba.suino.webapi.Domain.DTO;

namespace arroba.suino.webapi.Infra.CrossCutting.Enum
{
    public static class Metodos
    {
        public enum Name
        {
            Usuario
        }

        private static IList<LinkDTO> links = new List<LinkDTO>{
            new LinkDTO{ innerText=Name.Usuario.ToString(), href="/usuario/{0}" }
        };


        public static void AddLink(this IList<LinkDTO> list, string verb, Name name, params string[] args)
        {
            if (list == null) list = new List<LinkDTO>();
            LinkDTO link = links.FirstOrDefault(l => l.innerText == name.ToString());
            if (link != null)
            {
                list.Add(new LinkDTO
                {
                    innerText = $"[{ verb.ToUpper() }] { link.innerText }",
                    href = string.Format(link.href, args)
                });
            }
        }
    }
}