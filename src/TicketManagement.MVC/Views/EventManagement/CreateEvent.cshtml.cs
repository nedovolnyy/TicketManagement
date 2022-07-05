// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TicketManagement.Common.DI;
using TicketManagement.Common.Entities;
using TicketManagement.Common.Identity;

namespace TicketManagement.MVC.Views.EventManagement
{
    public class CreateEventModel : PageModel
    {
        private readonly IServiceProvider _serviceProvider;

        public CreateEventModel(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string Name { get; set; }

        public DateTimeOffset EventTime { get; set; }

        public string Description { get; set; }

        [BindProperty(Name = "layoudsId")]
        public List<string> LayoutId { get; set; }

        public DateTime EventEndTime { get; set; }

        public string EventLogoImage { get; set; }

        public decimal Price { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public IActionResult OnGet(List<string> layoutsId)
        {
            Input = new InputModel
            {
                LayoutId = layoutsId,
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(List<string> layoutsId)
        {
            foreach (var layoutId in layoutsId)
            {
            await _serviceProvider.GetRequiredService<IEventService>().InsertAsync(
                new Event(
                id: default,
                name: Input.Name,
                eventTime: DateTime.Now,
                description: Input.Description,
                eventEndTime: Input.EventEndTime,
                eventLogoImage: Input.EventLogoImage,
                layoutId: int.Parse(layoutId)),
                price: Price);
            }

            StatusMessage = "Event has been created";
            return RedirectToPage();
        }

        public class InputModel
        {
            [Required]
            [Display(Name = "Event Name")]
            public string Name { get; set; } = "wffwef";

            [Required]
            [Display(Name = "Event Time")]
            public DateTimeOffset EventTime { get; set; } = DateTimeOffset.Now;

            [Required]
            [Display(Name = "Description")]
            public string Description { get; set; } = "dfwefefw";

            [Required]
            public List<string> LayoutId { get; set; }

            [Required]
            [Display(Name = "Event End Time")]
            public DateTime EventEndTime { get; set; } = DateTime.Now;

            [Required]
            [Display(Name = "EventLogoImage")]
            public string EventLogoImage { get; set; } = "sdfsfwf";

            [Required]
            [DataType(DataType.Currency)]
            [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
            [Display(Name = "Price")]
            public decimal Price { get; set; } = decimal.One;
        }
    }
}
