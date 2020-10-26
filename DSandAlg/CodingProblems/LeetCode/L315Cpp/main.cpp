#include <iostream>
#include <vector>
#include <set>

// SS: problem John and I did Tuesday, May 5th 2020

class SolutionJohn {
public:
    std::vector<int> countLarger(std::vector<int>& nums) {
        std::multiset<int> set;
        std::vector<int> result(nums);
        for (int i = nums.size() - 1; i >= 0; i--) {
            int value = nums[i];

            auto i1 = set.lower_bound(value + 1);
            auto i2 = set.upper_bound(std::numeric_limits<int>::max());
            int dst = std::distance(i1, i2);
            result[i] = dst;

            set.insert(value);
        }

        return result;
    }
};

class Solution {
public:
    std::vector<int> countSmaller(std::vector<int>& nums) {
        std::multiset<int> set;
        std::vector<int> result(nums);
        for (int i = nums.size() - 1; i >= 0; i--) {
            int value = nums[i];

            auto i1 = set.lower_bound(std::numeric_limits<int>::min());
            auto i2 = set.lower_bound(value);
            int dst = std::distance(i1, i2);
            result[i] = dst;

            set.insert(value);
        }

        return result;
    }
};

int main() {
    auto input = std::vector<int>{9, 6, 8, 10, 7};
    auto solution = SolutionJohn{}.countLarger(input);

    input = std::vector<int>{5, 2, 6, 1};
    solution = Solution{}.countSmaller(input);

    return 0;
}
