using System.Collections.Generic;
using System.Diagnostics;
using RanOpt.iBuilding.Common.WpfControl.Core;

namespace RanOpt.iBuilding.Common.WpfControl.SelectableTreeView.ViewModels
{
    /// <summary>
    /// The basic view model for tree node, which contains the node's text and object
    /// </summary>
    [DebuggerDisplay("{" + nameof(NodeText) + "}")]
    public class TreeNodeBaseViewModel : AbstractNotifiableViewModel
    {
        private readonly List<TreeNodeBaseViewModel> _nodes = new List<TreeNodeBaseViewModel>();
        private object _nodeObj;
        private string _nodeText = "Root";
        internal List<object> SubItems { get; } = new List<object>();

        public string NodeText
        {
            get => _nodeText;
            set
            {
                _nodeText = value;
                OnPropertyChanged(nameof(NodeText));
            }
        }

        public object NodeObj
        {
            get => _nodeObj;
            set
            {
                _nodeObj = value;
                OnPropertyChanged(nameof(NodeObj));
            }
        }

        public IEnumerable<TreeNodeBaseViewModel> Nodes => _nodes;

        public virtual void AddNode(TreeNodeBaseViewModel node)
        {
            _nodes.Add(node);
            OnPropertyChanged(nameof(Nodes));
        }

        public void RemoveNode(TreeNodeBaseViewModel node)
        {
            _nodes.Remove(node);
            OnPropertyChanged(nameof(Nodes));
        }
    }
}