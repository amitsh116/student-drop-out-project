Submitted by:

# Amit Shchory

# Aviad Bloch

# Mickey Frankel


Contents:

# Data: All data that we used (and scripts we used for processing) are located at the directory "./data".
        Original dataset of raw data was obtained from this URL:
		https://archive.ics.uci.edu/dataset/697/predict+students+dropout+and+academic+success


# Clustering-based analysis source code:

  * Code used for clustering is located at "./scripts/agglomerative_clustering.py".


# ML prediction and statistical analysis:

  * Code used for analysis is located at "./scripts/Statistical_Analysis.py".


# Recommendation System:

  * System is cached at "./cache". Without cache, system training may take about 3 minutes.
    With cache, recommendation may take up to 15 seconds per student's course (so a new set of recommendations will load every 15 
	seconds or so).
	If absent, cache is automatically created on first run.
	
  * Command-line interface (CLI) is located at "./scripts/recommendation.py".
    For execution instructions, see documentation of recommendation module (beggining of "./scripts/recommendation.py").
	CLI allows for recommendation based on a single course the student took, as provided in the dataframe.
	
  * Graphical user interface (GUI):
  
    - Demo recording is available at: https://drive.google.com/file/d/122B8OQda191DNndFdA1DesALPGYMWE5R/view?usp=sharing
  
    - "./RecSys GUI source" contains all GUI source code.
	  See list of library requirements below.
	
	- Compiled version for Windows (64 bit): "./RecSys GUI (Windows)/RecommendationGUI.Desktop.exe".
	  .NET8.0 may be required in order to run.
	
	- Compiled version for Linux (64 bit): "./RecSys GUI (Linux)/RecommendationGUI.Desktop".
	  .NET8.0 may be required in order to run.
	
	- Compiled version for MacOS (64 bit): "./RecSys GUI (Mac)/RecommendationGUI.Desktop".
	  .NET8.0 may be required in order to run.
	
	- For compiled versions, all needed libraries are already included as shared object files.
	  Library requirements used (installed via NuGet):
	  @ Avalonia, Avalonia.Fonts.Inter, Avalonia.ReactiveUI, Avalonia.Themes.Fluent [All on version 11.1.2]
	  @ Deadpike.AvaloniaProgressRing [Version 0.10.8]
	  @ LiveChartsCore.SkiaSharpView.Avalonia [Version 2.0.0-rc2 (pre-release)]
	  @ MessageBox.Avalonia [Version 3.1.6]
	  @ Newtonsoft.Json [Version 13.0.3]
  
  * Recommendation accuracy evaluation: Located at "./scripts/recsys_eval.py".
    Accuracy is calculated as `1 - error`, with `error` being the absolute difference between the recommendation system's estimated 
	improvement rate and the improvement rate resulted from applying the recommended action/course on the student (as predicted
	by the same predictor used for building the system's utility matrices).
	Baseline comparison is in the same script.
	See module documentation.
	
	- For convenience, this evaluation script's output is at "./scripts/recsys_eval_output.txt".


# Any other files located in "./scripts" are utility modules called by the modules mentioned above, mostly the recommendation system.

  * See module documentation of each.


# Writeup is submitted separately, as per instructions.
