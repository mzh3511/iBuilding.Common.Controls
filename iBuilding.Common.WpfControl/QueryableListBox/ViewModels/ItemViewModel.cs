//  ****************************************************************************
//  Ranplan Wireless Network Design Ltd.
//  __________________
//   All Rights Reserved. [2018]
//  
//  NOTICE:
//  All information contained herein is, and remains the property of
//  Ranplan Wireless Network Design Ltd. and its suppliers, if any.
//  The intellectual and technical concepts contained herein are proprietary
//  to Ranplan Wireless Network Design Ltd. and its suppliers and may be
//  covered by U.S. and Foreign Patents, patents in process, and are protected
//  by trade secret or copyright law.
//  Dissemination of this information or reproduction of this material
//  is strictly forbidden unless prior written permission is obtained
//  from Ranplan Wireless Network Design Ltd.
// ****************************************************************************

using System;
using RanOpt.iBuilding.Common.WpfControl.Core;

namespace RanOpt.iBuilding.Common.WpfControl.QueryableListBox.ViewModels
{
    public class ItemViewModel : AbstractNotifiableViewModel
    {
        private bool _isSelected;
        private string _text;
        private bool _visible = true;
        public object Content { get; set; }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                NotifyParentIsSelectedChanged(this);
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                OnPropertyChanged(nameof(Visible));
            }
        }

        internal Action<ItemViewModel> IsSelectedChangedNotification { get; set; }

        /// <summary>
        /// Report to its parent that its <see cref="IsSelected" /> property has changed.
        /// </summary>
        /// <param name="obj"> The object which <see cref="IsSelected" /> property changed </param>
        protected virtual void NotifyParentIsSelectedChanged(ItemViewModel obj)
        {
            IsSelectedChangedNotification?.Invoke(obj);
        }
    }
}