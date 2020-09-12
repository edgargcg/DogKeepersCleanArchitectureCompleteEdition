using DogKeepers.Shared.Metadata;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace DogKeepers.Client.Shared.Components.Utils.Pagination
{
    public partial class Pagination
    {

        [Parameter] public PaginationMetadata PaginationData { get; set; }
        [Parameter] public EventCallback<int> SelectedPage { get; set; }

        public List<PagingLink> Links { get; set; }

        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }

        private void CreatePaginationLinks()
        {
            Links = new List<PagingLink>();
            var initPageNumber = PaginationData.CurrentPage - 2;
            var endPageNumber = PaginationData.CurrentPage + 2;

            Links.Add(new PagingLink(PaginationData.CurrentPage - 1, "<", PaginationData.HasPreviousPage));


            for(int pageCounter = initPageNumber; pageCounter <= endPageNumber; pageCounter++)
            {
                if (pageCounter > 0 && pageCounter <= PaginationData.TotalPages)
                    Links.Add(new PagingLink(pageCounter, pageCounter.ToString(), !(pageCounter == PaginationData.CurrentPage)));
            }

            Links.Add(new PagingLink(PaginationData.CurrentPage + 1, ">", PaginationData.HasNextPage));
        }

        public async Task OnSelectPage(PagingLink link)
        {
            if (link.PageNumber != PaginationData.CurrentPage && link.IsEnabled)
            {
                PaginationData.CurrentPage = link.PageNumber;
                await SelectedPage.InvokeAsync(link.PageNumber);
            }
        }

    }
}
