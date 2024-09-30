"""
Clustering script file.
"""


import pandas as pd
import numpy as np
from sklearn.preprocessing import StandardScaler
from sklearn.cluster import AgglomerativeClustering
from sklearn.decomposition import PCA
import matplotlib.pyplot as plt
from scipy.cluster.hierarchy import dendrogram
from sklearn.metrics import silhouette_score

data = pd.read_csv('../data/cleaned_data.csv')
# excluding categorical features
numeric_columns = data.select_dtypes(include=[np.number]).columns
categorical_columns = data.select_dtypes(exclude=[np.number]).columns
print("Categorical columns excluded:", categorical_columns.tolist())
# preprocess the data
data_numeric = data[numeric_columns].fillna(0)
# normalize the data
scaler = StandardScaler()
data_scaled = pd.DataFrame(scaler.fit_transform(data_numeric), columns=data_numeric.columns)
# feature selection based on correlation (only for numeric features)
correlation_matrix = data_scaled.corr().abs()
upper_tri = correlation_matrix.where(np.triu(np.ones(correlation_matrix.shape), k=1).astype(bool))
to_drop = [column for column in upper_tri.columns if any(upper_tri[column] > 0.95)]
data_scaled_uncorrelated = data_scaled.drop(to_drop, axis=1)
# PCA
pca = PCA(n_components=3)
pca_result = pca.fit_transform(data_scaled_uncorrelated)
# get feature names and their contributions to each PC
feature_names = data_scaled_uncorrelated.columns
pc_feature_contributions = []
for i in range(3):
    pc_loadings = pca.components_[i]
    sorted_indices = np.argsort(np.abs(pc_loadings))[::-1]
    top_features = [(feature_names[idx], pc_loadings[idx]) for idx in sorted_indices]
    pc_feature_contributions.append(top_features)
print("Principal Components and their contributing columns:")
for i, contributions in enumerate(pc_feature_contributions):
    print(f"PC{i + 1} (Explained Variance: {pca.explained_variance_ratio_[i]:.2%}):")
    for feature, loading in contributions:
        print(f"  {feature}: {loading:.4f}")
    print()
print("Cumulative explained variance:", np.sum(pca.explained_variance_ratio_))
# Function to plot dendrogram
def plot_dendrogram(model, **kwargs):
    counts = np.zeros(model.children_.shape[0])
    n_samples = len(model.labels_)
    for i, merge in enumerate(model.children_):
        current_count = 0
        for child_idx in merge:
            if child_idx < n_samples:
                current_count += 1  # leaf node
            else:
                current_count += counts[child_idx - n_samples]
        counts[i] = current_count
    linkage_matrix = np.column_stack([model.children_, model.distances_,
                                      counts]).astype(float)
    dendrogram(linkage_matrix, **kwargs)
# optimal number of clusters using the silhouette score
max_clusters = 30
silhouette_scores = []
for n_clusters in range(2, max_clusters + 1):
    clusterer = AgglomerativeClustering(n_clusters=n_clusters)
    cluster_labels = clusterer.fit_predict(pca_result)
    silhouette_scores.append(silhouette_score(pca_result, cluster_labels))
# Plot elbow curve
plt.figure(figsize=(10, 5))
plt.plot(range(2, max_clusters + 1), silhouette_scores, marker='o')
plt.xlabel('Number of clusters')
plt.ylabel('Silhouette Score')
plt.title('Elbow Method for Optimal k')
plt.show()
# choose the optimal number of clusters
optimal_clusters = silhouette_scores.index(max(silhouette_scores)) + 2
print(f"Optimal number of clusters: {optimal_clusters}")
# perform Agglomerative Clustering with optimal number of clusters
agg_clustering = AgglomerativeClustering(n_clusters=optimal_clusters)
clusters = agg_clustering.fit_predict(pca_result)
# visualize the results in 3D
fig = plt.figure(figsize=(12, 9))
ax = fig.add_subplot(111, projection='3d')
# a scatter plot for each cluster
for i in range(optimal_clusters):
    cluster_mask = clusters == i
    ax.scatter(pca_result[cluster_mask, 0],
               pca_result[cluster_mask, 1],
               pca_result[cluster_mask, 2],
               label=f'Cluster {i + 1}')
ax.set_xlabel(f"PC1 ({pca.explained_variance_ratio_[0]:.2%})\nTop features: {', '.join([f[0] for f in pc_feature_contributions[0][:3]])}")
ax.set_ylabel(f"PC2 ({pca.explained_variance_ratio_[1]:.2%})\nTop features: {', '.join([f[0] for f in pc_feature_contributions[1][:3]])}")
ax.set_zlabel(f"PC3 ({pca.explained_variance_ratio_[2]:.2%})\nTop features: {', '.join([f[0] for f in pc_feature_contributions[2][:3]])}")
plt.title('3D Agglomerative Clustering Visualization')
plt.legend(loc='upper left', bbox_to_anchor=(1, 1))
plt.tight_layout()
plt.show()
# dendrogram plotting
plt.figure(figsize=(10, 7))
agg_clustering_dendro = AgglomerativeClustering(distance_threshold=0, n_clusters=None)
agg_clustering_dendro = agg_clustering_dendro.fit(pca_result)
plot_dendrogram(agg_clustering_dendro, truncate_mode='level', p=3)
plt.title('Dendrogram')
plt.xlabel('Number of points in node')
plt.show()

