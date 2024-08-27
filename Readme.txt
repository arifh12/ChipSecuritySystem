You recently had a state of the art security system installed in your home. The master control panel requires a series of bi?colored chips to be placed end to end in a specific sequence in order to gain access. The security provider split up the chips and gave a random number to each of your family members. All of you must convene in order to assemble the chips and create the correct color combination. The access panel has a channel for the security chips. On each end of the channel is a colored marker starting with blue and ending with green. Chips are placed end to end such that the adjacent colors match and the starting and ending chips are color matched to the corresponding markers.

You are given a set of bi-colored chips and an empty channel the chips can be slotted into. On each end of the channel is a colored marker, with the starting marker being blue and the ending marker being green. Chips are to be placed end to end into the channel such that the adjacent colors match and the starting and ending chips are color matched to the corresponding markers. As an example, you are given the following chips:

[Blue, Yellow]
[Red, Green]
[Yellow, Red]
[Orange, Purple]

The result that successfully links the blue and green markers is:

Blue [Blue,Yellow] [Yellow, Red] [Red, Green] Green

In this instance, [Orange, Purple] is not used.

Your task is to write an algorithm that can successfully determine whether a given bag of color coded chips can link from end to end. 

You may NOT modify Color.cs, ColorChip.cs or Constants.cs. 

You MAY modify Program.cs and add any amount of code among any number of files to successfully complete the task.

You can assume the beginning marker is always Blue, and the ending marker is always Green.

You do not need to find ALL solutions. Any solution is acceptable, but the solution must use the most number of chips.

Asking questions is allowed and there are no tricks or gotchas that would trip you up. 
