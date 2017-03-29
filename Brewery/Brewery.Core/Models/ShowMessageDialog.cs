using System;

namespace Brewery.Core.Models
{
    public class ShowMessageDialog
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public Action OkButtonCommand { get; set; }

        public Action CancelButtonCommand { get; set; }
    }
}