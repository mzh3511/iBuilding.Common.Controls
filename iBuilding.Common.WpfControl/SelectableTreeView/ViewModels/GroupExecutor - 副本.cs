using System.Collections.Generic;
using System.Linq;

namespace RanOpt.iBuilding.Common.WpfControl.SelectableTreeView.ViewModels
{
    public class GroupExecutor<TItem>
    {
        private readonly IList<IGroupBy<TItem>> _groupBys;
        private readonly IGroupBy<TItem> _itemBy;

        public GroupExecutor(IList<IGroupBy<TItem>> groupBys, IGroupBy<TItem> itemBy)
        {
            _groupBys = groupBys;
            _itemBy = itemBy;
        }

        public TreeViewViewModel Execute(IList<TItem> items)
        {
            var result = new TreeViewViewModel();
            if (items == null || items.Count == 0)
                return result;

            var root = new TreeNodeViewModel();
            GroupBy(root, items, 0);
            foreach (var node in root.Nodes.ToList())
            {
                result.AddNode(node);
                root.RemoveNode(node);
            }
            return result;
        }

        private void GroupBy(TreeNodeViewModel parentNode, IList<TItem> groupItems, int groupIndex)
        {
            if (groupIndex >= _groupBys.Count)
            {
                _itemBy.Execute(parentNode, parentNode.SubItems.OfType<TItem>().ToList());
                return;
            }

            var groupExecutor = _groupBys[groupIndex];
            groupExecutor.Execute(parentNode, groupItems);

            foreach (var node in parentNode.Nodes)
                GroupBy(node, node.SubItems.OfType<TItem>().ToList(), groupIndex + 1);
        }
    }
}