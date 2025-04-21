function wordFrequency(text) {
  const words = text.toLowerCase().match(/\b\w+\b/g);
  if (!words) return [];

  const frequencyMap = words.reduce((acc, word) => {
    acc[word] = (acc[word] || 0) + 1;
    return acc;
  }, {});

  const sortedWords = Object.entries(frequencyMap)
    .sort((a, b) => b[1] - a[1])
    .map(([word, count]) => ({ word, count }));

  return sortedWords;
}

module.exports = wordFrequency;
