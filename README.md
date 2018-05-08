# Unity360PlayerForFove
A video player for the Fove headset implemented with their Unity plugin.

# What is fove?
Here you go: https://www.getfove.com/

# What's this?
Just a demo project on how to make a 360 VR player in Unity, because Fove does not have a native video player, and also lacks Unity demos.

# How do I use this?
This shouldn't be used as a daily 360 video player, it's quite bad (Unity isn't a video player) in terms of UI, options, etc. 

To use this you will have to install [Unity](https://store.unity.com/), the free version will do fine. Also install the Fove Drivers.

Then you can configure the videos in the `VideoManager` object in the scene. You can add your videos into the `VideoClip` list on this object. Make sure the videos are within the `/Asset` directory, this repo does not come with free videos. 

Hit play. That's it.

Hit `Esc` for some options and to skip stuff.

# What's so good about it
It's pretty simple but it has some neat features like eye tracking which Fove provides. You won't see it in the headset, but on your PC screen you can see two dots flying around which represent your left and right eyes. So get someone to try it and you can observe what they are observing.

# To do
A way to choose what you want to play will be nice instead of having to install unity and all that
