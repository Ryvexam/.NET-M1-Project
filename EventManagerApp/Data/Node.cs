namespace MonApp.Data
{
    public class Node
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NodeId { get; set; }
        public Node ParentNode { get; set; }
        public IList<Node> Children { get; set; }
        public int ParentNodeId { get; set; }
    }

}
