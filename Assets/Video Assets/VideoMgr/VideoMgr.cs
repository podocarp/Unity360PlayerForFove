/* Script that controls pretty much everything in the player.
 * TODO: Split the UI controls into another file
 */


using System.Collections.Generic;
using UnityEngine;

public class VideoMgr : MonoBehaviour {

    public UnityEngine.Video.VideoPlayer videoPlayer;
    public Material skyboxShader;

    public GameObject foveRig;
    public FoveInterface foveInterface;
    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc;
    //UI elements
    public UnityEngine.UI.Button playBtn;
    public UnityEngine.UI.Slider progBar;
    public GameObject playlist;
    public GameObject menu;
    public GameObject settings;
    public UnityEngine.UI.Text playlistText;
    
    private bool isPlaying = true;

    public List<UnityEngine.Video.VideoClip> videoClips;
    public int currentClipIndex = 0;
    private int maxClipIndex;


	// Use this for initialization
	void Start () {
        UnityEngine.Assertions.Assert.raiseExceptions=true;
        UnityEngine.Assertions.Assert.AreNotEqual(0, videoClips.Count);
        maxClipIndex = videoClips.Count - 1;
        SwitchVid(0);
        foveInterface.orientation = false;
        foveInterface.position = false;

    }

    private float gap = 0;
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) TogglePlay();
        if (Input.GetKeyDown(KeyCode.Escape)) ToggleMouseFocus();
        
        playlist.SetActive(Cursor.visible);
        menu.SetActive(Cursor.visible);

        //All this unnecessary code is due to Unity's incompetence
        //If you click "Next" Unity will load the next video.
        //However before it does that it clears the videoplayer out
        //resulting in frame=0 and framecount=0 causing anywhere from 2 to 5 videos being skipped.
        //Thus we add a small negligible lag to avoid this issue.
        //Please do not remove this seemingly chunk useless code.
        if (!videoPlayer.isLooping && ((ulong)videoPlayer.frame == videoPlayer.frameCount))
        {
            gap += Time.deltaTime;
            if (gap > .5f) {SwitchVid(1); gap = 0; }
        }

        progBar.value = (float)videoPlayer.frame/ (float)videoPlayer.frameCount;

        UpdatePlaylist();
	}

    public void UpdatePlaylist()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("Playlist\n");
        for (int i = 0; i < videoClips.Count; i++)
        {
            if (i == currentClipIndex) sb.Append("*");
            else sb.Append(" ");
            sb.Append(i + 1).Append(". ").Append(videoClips[i].name).Append("\n");
            
        }
        playlistText.text = sb.ToString();
    }
    /// <summary>
    /// Allows user to avoid playing this video again if they dislike it.
    /// </summary>
    public void RemoveVideo()
    {
        if (videoClips.Count == 1) Application.Quit();  //it was all you had and you threw it away

        //take out the current one and replace it.
        videoClips.RemoveAt(currentClipIndex);
        SwitchVid(0);
    }

    public void ToggleHeadsetControls()
    {
        foveInterface.orientation = !foveInterface.orientation;
        foveInterface.position =!foveInterface.position;
        fpc.enabled = !fpc.enabled; //if headset is in control we release mouse controls.
    }

    public void ToggleMouseFocus()
    {
        settings.SetActive(false);
        fpc.m_MouseLook.ToggleMouseFocus();
    }

    /// <summary>
    ///Function called when sliderbar is dragged (scrobble feature)
    /// </summary>
    public void Drag()
    {
        videoPlayer.Pause();
        videoPlayer.frame = (long)(progBar.value * videoPlayer.frameCount);
    }
    public void StopDrag()
    {
        videoPlayer.Play();
    }

    /// <summary>
    /// Function called when loop button is pressed
    /// </summary>
    public void ToggleLoop()
    {
        videoPlayer.isLooping = !videoPlayer.isLooping;
    }

    /// <summary>
    /// Function called when play/pause button is pressed
    /// </summary>
    public void TogglePlay()
    {
        if (isPlaying) {
            videoPlayer.Pause();
            playBtn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Play";
        }
        else {
            videoPlayer.Play();
            playBtn.GetComponentInChildren<UnityEngine.UI.Text>().text = "Pause";
        }
        isPlaying = !isPlaying;
    }

    /// <summary>
    /// Clamps an int [0,max]
    /// </summary>
    /// <param name="value"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    private int OverflowClamp(int value, int max)
    {
        if (value > max) return 0;
        else if (value < 0) return max;
        else return value;
    }

    /// <summary>
    /// Skips current playing video depending on arguments.
    /// </summary>
    /// <param name="increment">1 for next, -1 for prev, or 0 for refreshing the VideoPlayer obj.</param>
    public void SwitchVid(int increment)
    {
        currentClipIndex = OverflowClamp(currentClipIndex + increment, maxClipIndex);
        Debug.Log(increment);
        videoPlayer.clip = videoClips[currentClipIndex];
    }

}
