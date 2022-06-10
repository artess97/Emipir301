// Phase 2
// An implementation of MovieCollection ADT
// 2022


using System;
using System.Collections.Generic;

//A class that models a node of a binary search tree
//An instance of this class is a node in a binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode lchild; // reference to its left child 
	private BTreeNode rchild; // reference to its right child


	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicates in this movie collection
public class MovieCollection : IMovieCollection
{
	private BTreeNode root; // movies are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of (different) movies currently stored in this movie collection 
	private List<IMovie> movies = new List<IMovie>(); // movies are stored in a movie list


	// get the number of movies in this movie colllection 
	// pre-condition: nil
	// post-condition: return the number of movies in this movie collection and this movie collection remains unchanged
	public int Number { get { return count; } }

	// constructor - create an object of MovieCollection object
	public MovieCollection()
	{
		root = null;
		count = 0;	
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	public bool IsEmpty() // Check if the movie collection is empty.
	{
		if (root == null)
        {
			return true;
		}
		return false;
	}

	// Private handle function for recursive insert into a binary tree.
	private void recursiveInsert(IMovie movie, BTreeNode r) 
    {
		BTreeNode newNode = new BTreeNode(movie);

		if (movie.CompareTo(r.Movie) < 0)
		{
			if (r.LChild == null) // Add the movie to the left side 
			{
				r.LChild = newNode;
				count++;
			}
			else
			{
				recursiveInsert(movie, r.LChild);
			}
		}
		else
		{
			if (r.RChild == null) // Add the movie to the right side 
			{
				r.RChild = newNode;
				count++;
			}
			else
			{
				recursiveInsert(movie, r.RChild);
			}
		}
    }

	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	public bool Insert(IMovie movie)
	{
		if(Search(movie)) // Check if the movie is already stored
        {
			Console.WriteLine("The movie is stored.");
			return false;
        }

		BTreeNode newNode = new BTreeNode(movie);

		if (root == null) // Add the first movie to the root
        {
			root = newNode;
			count++;
			return true;
        }
        else
        {
			recursiveInsert(movie, root); // Use the recursive insert method to add the movie to the binary tree
			return true;
        }
	}

	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	public bool Delete(IMovie movie) //reference from BSTreeADT Activity and lecture 5 slides
	{

		BTreeNode ptr = root;
		BTreeNode parent = null;

		while ((ptr != null) && (movie.CompareTo(ptr.Movie) != 0))
		{
			parent = ptr;
			if (movie.CompareTo(ptr.Movie) < 0) // move to the left child of ptr
            {
				ptr = ptr.LChild;
			}
            else
            {
				ptr = ptr.RChild;
			}	
		}

		if (ptr != null) // if the search was successful
		{
			// Case 3: movie has two children
			if ((ptr.LChild != null) && (ptr.RChild != null))
			{
				// find the right-most node in left subtree of ptr
				if (ptr.LChild.RChild == null) // a special case: the right subtree of ptr.LChild is empty
				{
					ptr.Movie = ptr.LChild.Movie;
					ptr.LChild = ptr.LChild.LChild;
					Console.WriteLine("Delete 1");
					count--;
					return true;
				}
				else
				{
					BTreeNode p = ptr.LChild;
					BTreeNode pp = ptr; // parent of p
					while (p.RChild != null)
					{
						pp = p;
						p = p.RChild;
					}
					// copy the movie at p to ptr
					ptr.Movie = p.Movie;
					pp.RChild = p.LChild;
					Console.WriteLine("Delete 2");
					count--;
					return true;
				}
			}
			else // cases 1 & 2: movie has no or only one child
			{
				BTreeNode c;
				if (ptr.LChild != null)
                {
					c = ptr.LChild;
				}
                else
                {
					c = ptr.RChild;
				}

				// remove node ptr
				if (ptr == root) { 
					root = c;
					Console.WriteLine("Delete 3");
					count--;
					return true;
				} 
				else
				{
					if (ptr == parent.LChild)
                    {
						parent.LChild = c;
					}
                    else
                    {
						parent.RChild = c;
					}
					Console.WriteLine("Delete 4");
					count--;
					return true;
				}
			}
		}
		Console.WriteLine("Movie not found.");
		return false;
	}

	// Private handle function for recursive search from a binary tree.
	private bool recursiveSearch(IMovie movie, BTreeNode r) 
	{
		if (r != null)
		{
			if (movie.CompareTo(r.Movie) == 0)
			{
				return true;
			}
			else if (movie.CompareTo(r.Movie) < 0)
			{
				return recursiveSearch(movie, r.LChild); //Search the left side
			}
			else
			{
				return recursiveSearch(movie, r.RChild); // Search the right side
			}
		}
		else
		{
			return false;
		}
	}

	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	public bool Search(IMovie movie)
	{
		Movie newTitle = (Movie)movie;
		if (recursiveSearch(newTitle, root)) // use the recursive search method to find out if the movie is already stored in the binary tree
        {
			Console.WriteLine("{0} is in the movie collection.", newTitle.Title);
			return true;
        }
		return false;
	}

	// Search movie position
	private IMovie recursiveFindMovie(IMovie movie, BTreeNode r)
    {
		if (movie.CompareTo(r.Movie) == 0)
		{
			return r.Movie;
		}
		else if (movie.CompareTo(r.Movie) < 0)
		{
			return recursiveFindMovie(movie, r.LChild); //Search the left side
		}
		else
		{
			return recursiveFindMovie(movie, r.RChild); // Search the right side
		}

	}
	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string movietitle)
	{
		Movie mvTitle = new Movie(movietitle);

		if (Search(mvTitle)) 
        {
			Console.WriteLine("Movie found in database.");
			return recursiveFindMovie(mvTitle, root);
        }
		Console.WriteLine("Movie does not exist in database.");
		return null;
	}

	// Private handle function that add movie to the movies list in the correct order.
	private void InOrderTraverse(BTreeNode root)  
	{
		if (root != null)
		{
			InOrderTraverse(root.LChild);
			movies.Add(root.Movie);
			InOrderTraverse(root.RChild);
		}
	}

	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	public IMovie[] ToArray() 
	{
		movies.Clear();
		InOrderTraverse(root);
		return movies.ToArray();
	}

	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	public void Clear()
	{
		root = null;
		count = 0;
		Console.WriteLine("The information is clear.");
	}
}






