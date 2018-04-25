using UnityEngine;

public class AudioSourceLoudnessTester : MonoBehaviour
{

    public AudioSource audioSource;
    public float updateStep = 1f;
    public int sampleDataLength = 256;

    private float currentUpdateTime = 0f;

    public float clipLoudness;
    private float[] clipSampleData;

    // Use this for initialization
    void Awake()
    {

        if (!audioSource)
        {
            Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
        }
        clipSampleData = new float[sampleDataLength];

    }

    // Update is called once per frame
   public float clipRange  (int row, int col)
    {

        currentUpdateTime += Time.deltaTime;
        if (currentUpdateTime >= updateStep)
        {
            currentUpdateTime = 0f;
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
            clipLoudness = 0f;
            //foreach (var sample in clipSampleData)
            for(int i = 0+6*row+6*col; i<12+ 6 * row + 6 * col; i++)
            {
              
                    clipLoudness += Mathf.Abs(clipSampleData[i]);
                
              
            }
          
        }
        return clipLoudness;

    }



}
