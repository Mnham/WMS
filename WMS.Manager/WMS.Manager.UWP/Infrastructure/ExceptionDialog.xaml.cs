using System;
using System.Text;

using Windows.UI.Xaml.Controls;

namespace WMS.Manager.Infrastructure
{
    /// <summary>
    /// Представляет диалоговое окно исключения.
    /// </summary>
    public sealed partial class ExceptionDialog : ContentDialog
    {
        /// <summary>
        /// Создает экземпляр класса <see cref="ExceptionDialog"/>.
        /// </summary>
        public ExceptionDialog(Exception ex)
        {
            InitializeComponent();
            Message.Text = GetInnerException(ex);
        }

        /// <summary>
        /// Возвращает сообщения внутренних исключений.
        /// </summary>
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