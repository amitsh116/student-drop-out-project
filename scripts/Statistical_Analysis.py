# # "Marital status;Application mode;Application order;Course;""Daytime/evening attendance";Previous qualification;Previous qualification (grade);
# # Nacionality;Mother's qualification;Father's qualification;Mother's occupation;Father's occupation;Admission grade;Displaced;Educational special needs;
# # Debtor;Tuition fees up to date;Gender;Scholarship holder;Age at enrollment;International;Curricular units 1st sem (credited);
# # Curricular units 1st sem (enrolled);Curricular units 1st sem (evaluations);Curricular units 1st sem (approved);Curricular units 1st sem (grade);
# # Curricular units 1st sem (without evaluations);Curricular units 2nd sem (credited);Curricular units 2nd sem (enrolled);
# # Curricular units 2nd sem (evaluations);
# # Curricular units 2nd sem (approved);Curricular units 2nd sem (grade);Curricular units 2nd sem (without evaluations);Unemployment rate;Inflation rate;GDP;
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt
import seaborn as sns
import scipy.stats as stats
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
from sklearn.metrics import accuracy_score, classification_report
import statsmodels.api as sm

# Load the data
file_path = "../data/cleaned_data.csv"
data = pd.read_csv(file_path)

# Data preprocessing
data = data.dropna(subset=['Target'])
data = data[data['Target'] != 'Enrolled']
data['Target'] = data['Target'].map({'Dropout': 1, 'Graduate': 0})

# Select features for analysis
features = ['Marital status', 'Nacionality', 'Gender', 'Age at enrollment', 'Admission grade',
            'Curricular units 1st sem (approved)', 'Curricular units 2nd sem (approved)',
            'Scholarship holder', 'Tuition fees up to date']

X = data[features]
y = data['Target']

# Convert categorical variables to dummy variables
X = pd.get_dummies(X, drop_first=True)

# Split the data
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

# 1. Hypothesis Testing: Chi-square test for Marital Status vs Dropout

marital_status_map = {
    1: 'Single',
    2: 'Married',
    3: 'Widower',
    4: 'Divorced',
    5: 'Facto Union',
    6: 'Legally Separated'
}

# Apply the mapping to create a new column
data['Marital Status'] = data['Marital status'].map(marital_status_map)

contingency_table = pd.crosstab(data['Marital Status'], data['Target'])
chi2, p_value, dof, expected = stats.chi2_contingency(contingency_table)

print("Chi-Square Test: Marital Status vs Dropout")
print(f"Chi-square statistic: {chi2}")
print(f"P-value: {p_value}")

# Calculate percentages
contingency_table_percent = contingency_table.div(contingency_table.sum(axis=1), axis=0) * 100

# Sort the data by dropout rate
contingency_table_percent_sorted = contingency_table_percent.sort_values(by=1, ascending=False)

# Set up the plot
plt.figure(figsize=(14, 8))
sns.set_style("whitegrid")

# Create the bar plot with adjusted width
ax = sns.barplot(x=contingency_table_percent_sorted.index,
                 y=contingency_table_percent_sorted[1],
                 color='#4A90E2',
                 alpha=0.8,
                 width=0.6)  # Adjust width here

# Customize the plot
plt.title('Dropout Rate by Marital Status', fontsize=22, pad=20)
plt.xlabel('Marital Status', fontsize=16, labelpad=10)
plt.ylabel('Dropout Rate (%)', fontsize=16, labelpad=10)
plt.xticks(rotation=45, ha='right', fontsize=14)
plt.yticks(fontsize=14)

# Add percentage labels above the bars
for i, v in enumerate(contingency_table_percent_sorted[1]):
    ax.text(i, v + 1, f'{v:.1f}%', ha='center', va='bottom', fontsize=14, fontweight='bold')

# Add overall dropout rate line
overall_dropout_rate = contingency_table_percent[1].mean()
plt.axhline(y=overall_dropout_rate, color='red', linestyle='--', linewidth=2)
plt.text(len(contingency_table_percent_sorted) - 1, overall_dropout_rate,
         f'Overall: {overall_dropout_rate:.1f}%',
         ha='right', va='bottom', color='red', fontweight='bold', fontsize=14)

# Adjust layout and display
plt.tight_layout()
plt.savefig('marital_status_dropout_rate_refined.png', dpi=300, bbox_inches='tight')
plt.close()

X_logit = sm.add_constant(X)
model = sm.Logit(y, X_logit)
results = model.fit()

print("\nLogistic Regression Results:")
print(results.summary())

# Confidence intervals for odds ratios
conf = results.conf_int()
conf['Odds Ratio'] = results.params
conf['Lower CI'] = np.exp(conf[0])
conf['Upper CI'] = np.exp(conf[1])
conf['Odds Ratio'] = np.exp(conf['Odds Ratio'])
conf = conf[['Odds Ratio', 'Lower CI', 'Upper CI']]
print("\nConfidence Intervals for Odds Ratios:")
print(conf)

# Remove the 'const' row and sort by Odds Ratio
conf = conf.drop('const')
conf_sorted = conf.sort_values('Odds Ratio')

# Visualize odds ratios and confidence intervals
plt.figure(figsize=(16, 10))  # Increased figure size
sns.set_style("whitegrid")

# Plot the odds ratios
ax = sns.barplot(x=conf_sorted.index, y='Odds Ratio', data=conf_sorted, color='skyblue')

# Add error bars
yerr = conf_sorted[['Lower CI', 'Upper CI']].sub(conf_sorted['Odds Ratio'], axis=0).abs().values.T
ax.errorbar(x=range(len(conf_sorted)), y=conf_sorted['Odds Ratio'], yerr=yerr,
            fmt='none', c='black', capsize=5)

plt.title('Odds Ratios with 95% Confidence Intervals', fontsize=20)
plt.xlabel('Features', fontsize=16)
plt.ylabel('Odds Ratio (log scale)', fontsize=16)
plt.yscale('log')
plt.xticks(rotation=45, ha='right', fontsize=14)
plt.yticks(fontsize=14)

# Add reference line at y = 1
plt.axhline(y=1, color='r', linestyle='--', alpha=0.7)
plt.text(plt.xlim()[1], 1, 'OR = 1', va='center', ha='left', backgroundcolor='w', color='r', fontsize=14)

# Add OR values above each bar with increased font size and distance
for i, v in enumerate(conf_sorted['Odds Ratio']):
    ax.text(i, v * 1.1, f'{v:.2f}', ha='center', va='bottom', fontweight='bold', fontsize=14)

plt.tight_layout()
plt.savefig('odds_ratios_ci_improved_readable.png', dpi=300, bbox_inches='tight')
plt.close() 



# 3. Random Forest Classifier
rf_model = RandomForestClassifier(n_estimators=100, random_state=42)
rf_model.fit(X_train, y_train)
y_pred = rf_model.predict(X_test)

print("\nRandom Forest Classifier Performance:")
print(classification_report(y_test, y_pred))

# Feature importance
feature_importance = pd.DataFrame({'feature': X.columns, 'importance': rf_model.feature_importances_})
feature_importance = feature_importance.sort_values('importance', ascending=False)

plt.figure(figsize=(10, 6))
sns.barplot(x='importance', y='feature', data=feature_importance)
plt.title('Feature Importance in Random Forest Model')
plt.tight_layout()
plt.savefig('feature_importance.png')
plt.close()

print("\nAnalysis complete. Visualizations have been saved as PNG files.")