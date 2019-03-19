using System;

namespace cs2
{
/// <summary>
/// Simple generic tree class.
/// This is an immutable, "open box" data structure,
/// with in-order, pre-order and post-order iterators.
/// </summary>
/// <remarks>
/// Author: George Wells
/// Version: 1.0 (23 September 2013)
/// </remarks>
public class Tree<T>
  { /// <summary>The data stored in this node.</summary>
    private T data;
    /// <summary>Pointer to left subtree.</summary>
    private Tree<T> lt;
    /// <summary>Pointer to right subtree.</summary>
    private Tree<T> rt;

    /// <summary>
    /// Creates new node with left and right subtrees.
    /// </summary>
    /// <param name="value">The value to be stored in this node.</param>
    /// <param name="left">The left subtree to be added to this node.</param>
    /// <param name="right">The right subtree to be added to this node.</param>
    public Tree (T value, Tree<T> left, Tree<T> right)
      { lt = left;
        rt = right;
        data = value;
      } // Constructor

    /// <summary>
    /// Creates new node without subtrees.
    /// </summary>
    /// <param name="value">The value to be stored in this node.</param>
    public Tree(T value) : this(value, null, null)
      {
      } // Constructor

    /// <summary>
    /// Return the left subtree of a tree.
    /// </summary>
    /// <returns>Left subtree of this node.</returns>
    public Tree<T> Left ()
      { return lt; }

    /// <summary>
    /// Return the right subtree of a tree.
    /// </summary>
    /// <returns>Right subtree of this node.</returns>
    public Tree<T> Right ()
      { return rt; }

    /// <summary>
    /// Add a left subtree to a node.
    /// <para><I>Precondition:</I> There is no left subtree.</para>
    /// </summary>
    /// <param name="left">The new left tree to be added.</param>
    /// <exception cref="System.InvalidOperationException">if there is a pre-existing left subtree.</exception>
    public void AddLeft (Tree<T> left)
      { if (lt != null)
          throw new InvalidOperationException("subtree already present");
        lt = left;
      } // AddLeft

    /// <summary>
    /// Add a right subtree to a node.
    /// <para><I>Precondition:</I> There is no right subtree.</para>
    /// </summary>
    /// <param name="right">The new right tree to be added.</param>
    /// <exception cref="System.InvalidOperationException">if there is a pre-existing right subtree.</exception>
    public void AddRight (Tree<T> right)
      { if (rt != null)
          throw new InvalidOperationException("subtree already present");
        rt = right;
      } // AddRight

    /// <summary>
    /// Access the data value in a node of a tree.
    /// </summary>
    /// <returns>The data contained in this tree node.</returns>
    public T GetData ()
      { return data; }

  } // class Tree

} // namespace cs2