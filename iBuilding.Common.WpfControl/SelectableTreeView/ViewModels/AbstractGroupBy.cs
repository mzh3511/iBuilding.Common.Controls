using System;
using System.Collections.Generic;
using System.Linq;

namespace RanOpt.iBuilding.Common.WpfControl.SelectableTreeView.ViewModels
{
    public abstract class AbstractGroupBy<TItem, TGroupKey> : IGroupBy<TItem>
    {
        protected AbstractGroupBy(Func<TGroupKey, string> textSelector, Comparison<TGroupKey> comparer)
        {
            TextSelector = textSelector;
            Comparer = comparer;
        }

        public Func<TGroupKey, string> TextSelector { get; }
        public Comparison<TGroupKey> Comparer { get; }

        public void Execute(TreeNodeViewModel root, IList<TItem> items)
        {
            if (root == null)
                throw new ArgumentNullException(nameof(root));
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (items.Count == 0)
                return;

            var groups = Group(root, items);
            if (Comparer != null)
                groups = groups.OrderBy(cond => cond.Key, Comparer<TGroupKey>.Create(Comparer));

            foreach (var group in groups)
            {
                var node = new TreeNodeViewModel { NodeText = TextSelector(group.Key), NodeObj = group.Key };
                node.SubItems.AddRange(group.OfType<object>());
                root.AddNode(node);
            }
        }

        protected abstract IEnumerable<IGrouping<TGroupKey, TItem>> Group(TreeNodeViewModel root,
            IList<TItem> items);
    }
}