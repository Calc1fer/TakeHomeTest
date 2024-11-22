# TakeHomeTest
 Take home test

*****************************************************************************************
Exercise 1 - Ball Prediction
I used Unity to set up a simple environment locked to the x and y axes. It showcases a ball that when clicking left mouse will be projected in a given direction. It can bounce off the walls while maintaining a constant velocity. A custom gravity value will bring it to the ground. 

The prediction function makes use of the quadratic equation related to vertical and horizontal motion. We use s= ut + 1/2 at^2 and the discriminant (sqrt)b2 - 4ac to determine if there any solution.

The position of the ball will be displayed by showing a translucent ball at the predicted position if this lies within the bounds of the level.



*****************************************************************************************
Exercise 2 - Jewel Matching
This script contains an attempt at finding the best match in a 3-match game vertically/horizontally.

Assuming a board has been set up with no matching tiles, the CalculateBestMoveForBoard function will iterate through the grid based on the movement of the jewel. The current and target jewel swap is simulated, where then the jewels are compared on either side of the current jewel position on the x and y axes. The score is incremented for however long the jewels match then the total score is returned. If this is the highest score on the board, then this is the best move. If there are multiple moves scoring the same then the first found best move will be returned.


*****************************************************************************************
Exercise 3 - 100 Objects
Enjoyable simple project where the user can move 3 predefined transforms to any location they want on the x and z axes. The camera is fixed to a top down perspective. One of the transforms has a collider and different colour to distinguish it as being the final position the ball object will move to (assuming the designer doesn't reorder the transforms, no problems will occur with the ball moving to all three positions).

The ball will move to each position in order, playing a oneshot sound and particle effect when reaching the goal.If the ball collides with one of the randomly placed objects in the scene, the ball is destroyed, a oneshot and a particle effect are played.
