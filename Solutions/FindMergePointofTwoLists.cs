// Find Merge Point of Two Lists


    // Complete the findMergeNode function below.

    /*
     * For your reference:
     *
     * SinglyLinkedListNode {
     *     int data;
     *     SinglyLinkedListNode next;
     * }
     *
     */
    static int findMergeNode(SinglyLinkedListNode head1, SinglyLinkedListNode head2) {
        if(IsVisited(head1)) return head1.data;
        if(IsVisited(head2)) return head2.data;

        return findMergeNode(head1?.next, head2?.next);
    }

    static bool IsVisited(SinglyLinkedListNode node)
    {
        if(node == null) return false;

        if(Cache.ContainsKey(node))
            return true;

        Cache.Add(node, true);

        return false;
    }

    static Dictionary<SinglyLinkedListNode, bool> Cache = new Dictionary<SinglyLinkedListNode, bool>();

