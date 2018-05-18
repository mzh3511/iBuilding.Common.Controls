using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;

namespace RanOpt.iBuilding.Common.WpfControl.SelectableTreeView.ViewModels
{
    public class TreeNodeViewModel : TreeNodeNotifiableViewModel
    {
        private bool _isSelected;
        private bool _visible = true;
        public new IEnumerable<TreeNodeViewModel> Nodes => base.Nodes.OfType<TreeNodeViewModel>();

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (value == _isSelected)
                    return;

                _isSelected = value;
                foreach (var node in Nodes)
                {
                    if (node.Visible)
                        node.IsSelected = value;
                }

                OnPropertyChanged(nameof(IsSelected));
                NotifyParentPropertyChanged(this, nameof(IsSelected));
            }
        }

        public bool Visible
        {
            get => _visible;
            set
            {
                if (value == _visible)
                    return;

                _visible = value;
                foreach (var node in Nodes)
                    node.Visible = value;

                OnPropertyChanged(nameof(Visible));
                NotifyParentPropertyChanged(this, nameof(Visible));
            }
        }

        public ICommand QueryCommand { get; }

        public TreeNodeViewModel()
        {
            QueryCommand = new DelegateCommand<string>(Query);
        }

        protected override bool OnSubPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(IsSelected))
            {
                var nodes = Nodes.ToList();
                var isSelected = nodes.Count != 0 && nodes.Count(node => node.IsSelected) == nodes.Count;

                if (isSelected != _isSelected)
                {
                    _isSelected = isSelected;
                    OnPropertyChanged(nameof(IsSelected));
                    return true;
                }
            }
            return base.OnSubPropertyChanged(propertyName);
        }


        private void Query(string condition)
        {
            var lowerCondition = string.IsNullOrEmpty(condition) ? string.Empty : condition.ToLower();

            if (string.IsNullOrEmpty(condition))
            {
                foreach (var node in Nodes)
                {
                    node.Visible = true;
                }
            }
            else
            {
                var nodes = new List<TreeNodeViewModel>();
                foreach (var node in Nodes)
                {
                    FetchAll(node, cond =>
                    {
                        if (cond.NodeText.ToLower().Contains(lowerCondition)) nodes.Add(cond);
                    });
                }
                foreach (var node in nodes)
                {
                    node.Visible = true;
                }
            }
        }

        private void FetchNodes(Action<TreeNodeViewModel> callback)
        {
            foreach (var subNode in Nodes)
            {
                FetchNodes(subNode, callback);
            }
        }
        private static void FetchNodes(TreeNodeViewModel node, Action<TreeNodeViewModel> callback)
        {
            if (node == null || callback == null)
                return;
            callback.Invoke(node);
            foreach (var subNode in node.Nodes)
            {
                FetchNodes(subNode, callback);
            }
        }
        /// <summary>
        /// Fetch the node's all subnodes with cascade
        /// </summary>
        /// <param name="node"></param>
        /// <param name="callback"></param>
        public static void FetchAll(TreeNodeViewModel node, Action<TreeNodeViewModel> callback)
        {
            if (node == null)
                return;
            callback.Invoke(node);
            foreach (var subNode in node.Nodes)
            {
                FetchAll(subNode, callback);
            }
        }

        /// <summary>
        /// Fetch the node's all subnodes with cascade until the <paramref name="callback"/> returns true
        /// </summary>
        /// <param name="node"></param>
        /// <param name="callback"></param>
        public static void FetchOne(TreeNodeViewModel node, Func<TreeNodeViewModel, bool> callback)
        {
            if (node == null)
                return;
            if (callback.Invoke(node))
                return;

            foreach (var subNode in node.Nodes)
            {
                FetchOne(subNode, callback);
            }
        }


        //private void SetIsSelected()
        //{
        //    var selection = GetSelection();
        //    var isSelected = selection.Length == _item.Count && _item.Count != 0;
        //    if (isSelected == _isSelected)
        //        return;

        //    _isSelected = isSelected;
        //    OnPropertyChanged(nameof(IsSelected));
        //}
    }
}