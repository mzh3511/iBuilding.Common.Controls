using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using RanOpt.iBuilding.Common.WpfControl.Core;

namespace RanOpt.iBuilding.Common.WpfControl.SelectableTreeView.ViewModels
{
    public class TreeViewViewModel: AbstractNotifiableViewModel
    {
        private bool _isSelected;
        private readonly List<TreeNodeViewModel> _nodes = new List<TreeNodeViewModel>();
        public IEnumerable<TreeNodeViewModel> Nodes => _nodes;

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
            }
        }

        public ICommand QueryCommand { get; }

        public TreeViewViewModel()
        {
            QueryCommand = new DelegateCommand<string>(Query);
        }

        public virtual void AddNode(TreeNodeViewModel node)
        {
            _nodes.Add(node);
            OnPropertyChanged(nameof(Nodes));
        }

        public void RemoveNode(TreeNodeViewModel node)
        {
            _nodes.Remove(node);
            OnPropertyChanged(nameof(Nodes));
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
    }

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
    }
}