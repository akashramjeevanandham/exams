const maxSubarraySum = require("./maxSubarraySum");
const wordFrequency = require("./wordFrequency");

console.log("Testing maxSubarraySum:");
const arr = [1, -2, 3, 4, -1, 2, 1, -5, 4];
console.log("Input array:", arr);
console.log("Maximum subarray sum:", maxSubarraySum(arr));

console.log("\nTesting wordFrequency:");
const text = "This is a test. This test is only a test.";
console.log("Input text:", text);
const freq = wordFrequency(text);
console.log("Word frequencies sorted by count:");
if (freq.length === 0) {
  console.log("No words found.");
} else {
  freq.forEach(({ word, count }) => {
    console.log(word + ": " + count);
  });
}
