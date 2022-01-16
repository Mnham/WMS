using System;
using System.Text;

using Windows.UI.Xaml.Controls;

namespace WMS.Manager.Infrastructure
{
    public sealed partial class ExceptionDialog : ContentDialog
    {
        public ExceptionDialog(Exception ex)
        {
            InitializeComponent();
            Message.Text = GetInnerException(ex);
        }

        private string GetInnerException(Exception ex)
        {
            StringBuilder result = new();
            result.AppendLine(ex.Message);
            if (ex.InnerException is not null)
            {
                result.Append(GetInnerException(ex.InnerException));
            }

            return result.ToString();
        }
    }
}