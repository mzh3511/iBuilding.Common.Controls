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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using RanOpt.iBuilding.Common.WpfControl.Core;

namespace RanOpt.iBuilding.Common.WpfControl.QueryableListBox.ViewModels
{
    public class ContainerViewModel : AbstractNotifiableViewModel
    {
        private readonly List<ItemViewModel> _item = new List<ItemViewModel>();
        private bool _isSelected;

        public ContainerViewModel()
        {
            QueryCommand = new DelegateCommand<string>(Query);
        }

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                foreach (var item in _item)
                {
                    if (item.Visible)
                        item.IsSelected = value;
                }
                _isSelected = value;
            }
        }

        public IList<ItemViewModel> Items => _item;

        public IList Selection
        {
            get => GetSelection();
            set => SetSelection(value);
        }

        public ICommand QueryCommand { get; }
        public void Add<T>(T content, string text)
        {
            var item = new ItemViewModel
            {
                Content = content,
                Text = text,
                IsSelected = false,
                IsSelectedChangedNotification = ReceiveSubIsSelectedChanged
            };
            _item.Add(item);
        }

        public void AddRange<T>(IEnumerable<T> collection, Func<T, string> getText)
        {
            foreach (var item in collection)
                Add(item, getText(item));
        }

        private void Query(string condition)
        {
            var lowerCondition = string.IsNullOrEmpty(condition) ? string.Empty : condition.ToLower();
            foreach (var item in _item)
                item.Visible = string.IsNullOrEmpty(condition) || item.Text.ToLower().Contains(lowerCondition);

            SetIsSelected();
        }

        private void SetSelection(IList selection)
        {
            var list = selection?.OfType<object>().ToList() ?? new List<object>();
            foreach (var item in _item)
                item.IsSelected = list.Any(cond => cond == item.Content);
        }

        private Array GetSelection()
        {
            return _item.Where(item => item.IsSelected).Select(item => item.Content).ToArray();
        }

        private void ReceiveSubIsSelectedChanged(ItemViewModel obj)
        {
            SetIsSelected();
            OnPropertyChanged(nameof(Selection));
        }

        private void SetIsSelected()
        {
            var selection = GetSelection();
            var isSelected = selection.Length == _item.Count && _item.Count != 0;
            if (isSelected == _isSelected)
                return;

            _isSelected = isSelected;
            OnPropertyChanged(nameof(IsSelected));
        }
    }
}