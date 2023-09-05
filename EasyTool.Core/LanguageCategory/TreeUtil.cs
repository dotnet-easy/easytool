using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyTool
{
    /// <summary>
    /// 树工具类，用于构建树结构
    /// </summary>
    public class TreeUtil<T, D>
    {
        private List<TreeNode<T, D>> _nodes;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodes">树节点列表</param>
        public TreeUtil(List<TreeNode<T, D>> nodes)
        {
            _nodes = nodes;
        }

        /// <summary>
        /// 构建树结构
        /// </summary>
        /// <returns>根节点</returns>
        public TreeNode<T, D> BuildTree()
        {
            // 获取根节点
            var root = _nodes.FirstOrDefault(n => n.ParentId.Equals(default(T)));

            if (root == null)
            {
                return null;
            }

            // 构建树结构
            BuildTree(root);

            return root;
        }

        private void BuildTree(TreeNode<T, D> node)
        {
            node.Children = _nodes.Where(n => n.ParentId.Equals(node.Id)).ToList();

            if (node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    BuildTree(child);
                }
            }
        }

        /// <summary>
        /// 获取某个节点的所有父节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>父节点列表，从根节点到该节点的顺序</returns>
        public List<TreeNode<T, D>> GetParents(TreeNode<T, D> node)
        {
            var parents = new List<TreeNode<T, D>>();
            var parent = GetParent(node.Id);
            while (parent != null)
            {
                parents.Insert(0, parent);
                parent = GetParent(parent.Id);
            }
            return parents;
        }

        /// <summary>
        /// 获取某个节点的深度
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>节点深度，根节点的深度为0</returns>
        public int GetDepth(TreeNode<T, D> node)
        {
            return GetParents(node).Count;
        }

        /// <summary>
        /// 获取某个节点的所有子孙节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>子孙节点列表</returns>
        public List<TreeNode<T, D>> GetDescendants(TreeNode<T, D> node)
        {
            var descendants = new List<TreeNode<T, D>>();
            GetDescendants(node, descendants);
            return descendants;
        }

        private void GetDescendants(TreeNode<T, D> node, List<TreeNode<T, D>> descendants)
        {
            descendants.Add(node);
            if (node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    GetDescendants(child, descendants);
                }
            }
        }

        private TreeNode<T, D> GetParent(T id)
        {
            return _nodes.FirstOrDefault(n => n.Id.Equals(id));
        }

        /// <summary>
        /// 获取某个节点的所有兄弟节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>兄弟节点列表</returns>
        public List<TreeNode<T, D>> GetSiblings(TreeNode<T, D> node)
        {
            var parent = GetParent(node.Id);
            if (parent == null)
            {
                return new List<TreeNode<T, D>>();
            }
            return parent.Children.Where(n => !n.Id.Equals(node.Id)).ToList();
        }

        /// <summary>
        /// 获取某个节点的所有兄弟节点数量
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>兄弟节点数量</returns>
        public int GetSiblingCount(TreeNode<T, D> node)
        {
            return GetSiblings(node).Count;
        }

        /// <summary>
        /// 判断某个节点是否是叶子节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>是否是叶子节点</returns>
        public bool IsLeaf(TreeNode<T, D> node)
        {
            return node.Children.Count == 0;
        }

        /// <summary>
        /// 获取树的最大深度
        /// </summary>
        /// <returns>树的最大深度</returns>
        public int GetMaxDepth()
        {
            return _nodes.Max(n => GetDepth(n));
        }

        /// <summary>
        /// 获取树的最小深度
        /// </summary>
        /// <returns>树的最小深度</returns>
        public int GetMinDepth()
        {
            return _nodes.Min(n => GetDepth(n));
        }

        /// <summary>
        /// 获取某个节点的下一个兄弟节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>下一个兄弟节点</returns>
        public TreeNode<T, D> GetNextSibling(TreeNode<T, D> node)
        {
            var siblings = GetSiblings(node);
            var index = siblings.FindIndex(n => n.Id.Equals(node.Id));
            return index + 1 < siblings.Count ? siblings[index + 1] : null;
        }

        /// <summary>
        /// 获取某个节点的上一个兄弟节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>上一个兄弟节点</returns>
        public TreeNode<T, D> GetPreviousSibling(TreeNode<T, D> node)
        {
            var siblings = GetSiblings(node);
            var index = siblings.FindIndex(n => n.Id.Equals(node.Id));
            return index - 1 >= 0 ? siblings[index - 1] : null;
        }

        /// <summary>
        /// 获取某个节点的首个子节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>首个子节点</returns>
        public TreeNode<T, D> GetFirstChild(TreeNode<T, D> node)
        {
            return node.Children.Count > 0 ? node.Children[0] : null;
        }

        /// <summary>
        /// 获取某个节点的最后一个子节点
        /// </summary>
        /// <param name="node">节点</param>
        /// <returns>最后一个子节点</returns>
        public TreeNode<T, D> GetLastChild(TreeNode<T, D> node)
        {
            return node.Children.Count > 0 ? node.Children[node.Children.Count - 1] : null;
        }

        /// <summary>
        /// 获取树的所有节点数量
        /// </summary>
        /// <returns>树的所有节点数量</returns>
        public int GetNodeCount()
        {
            return _nodes.Count;
        }

        /// <summary>
        /// 获取树的所有叶子节点数量
        /// </summary>
        /// <returns>树的所有叶子节点数量</returns>
        public int GetLeafCount()
        {
            return _nodes.Count(IsLeaf);
        }

        /// <summary>
        /// 获取树的所有节点的权重和
        /// </summary>
        /// <returns>树的所有节点的权重和</returns>
        public int GetTotalWeight()
        {
            return _nodes.Sum(n => n.Weight);
        }

        /// <summary>
        /// 获取树的所有叶子节点的权重和
        /// </summary>
        /// <returns>树的所有叶子节点的权重和</returns>
        public int GetLeafWeightTotal()
        {
            return _nodes.Where(IsLeaf).Sum(n => n.Weight);
        }

        /// <summary>
        /// 获取树的平均深度
        /// </summary>
        /// <returns>树的平均深度</returns>
        public int GetAverageDepth()
        {
            return (int)_nodes.Average(n => GetDepth(n));
        }

        /// <summary>
        /// 获取树的平均节点权重
        /// </summary>
        /// <returns>树的平均节点权重</returns>
        public int GetAverageWeight()
        {
            return (int)_nodes.Average(n => n.Weight);
        }

        /// <summary>
        /// 获取树的最大节点权重
        /// </summary>
        /// <returns>树的最大节点权重</returns>
        public int GetMaxWeight()
        {
            return _nodes.Max(n => n.Weight);
        }

        /// <summary>
        /// 获取树的最小节点权重
        /// </summary>
        /// <returns>树的最小节点权重</returns>
        public int GetMinWeight()
        {
            return _nodes.Min(n => n.Weight);
        }
    }

    public class TreeNode<T, D>
    {
        public T Id { get; set; }
        public T ParentId { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public D Data { get; set; }
        public List<TreeNode<T, D>> Children { get; set; }

        public TreeNode(T id, T parentId, string name, int weight, D data)
        {
            this.Id = id;
            this.ParentId = parentId;
            this.Name = name;
            this.Weight = weight;
            this.Data = data;
        }
    }
}
