using System.Collections;

namespace DigitalProduction.Algorithms;

/// <summary>
/// A collection of general purpose algorithms.
/// </summary>
public static class Algorithms
{
	/// <summary>
	/// Randomize an array list.  Uses .Net framework "Random" object to generate random numbers.
	/// Written May 2004.
	/// </summary>
	/// <param name="list">Array to be shuffled.</param>
	public static void Shuffle(ref ArrayList list)
	{
		// Random number generator.  Generate a new generator each time to increase
		// the amount of entropy in the system calling the shuffle function.
		Random random = new(DateTime.Now.Millisecond);

		// Do the shuffle.
		int shuffles = random.Next(70, 90);

		for (int i = 0; i < shuffles; i++)
		{
			// Make a complete pass through all the cards switch the card at
			// each position with one in a random position.  This ensures every
			// card is touched at least once per time through the cards.
			for (int j = 0; j < list.Count; j++)
			{
				object? obj = list[j];

				int index = random.Next(0, list.Count-1);
				list[j] = list[index];

				list[index] = obj;
			}
		}
	}

} // End class.