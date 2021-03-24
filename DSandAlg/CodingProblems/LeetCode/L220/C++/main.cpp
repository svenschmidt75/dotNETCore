#include <iostream>
#include <vector>
#include <set>

class Solution {
public:
    bool containsNearbyAlmostDuplicate(std::vector<int> &nums, int k, int t) {
        // SS: time-limit exceeded

        if (k == 0) {
            return false;
        }

        std::set<std::int64_t> s;

        for (int i = 0; i < nums.size(); ++i) {
            if (i > 0) {
                std::int64_t min = (std::int64_t)nums[i] - t;
                std::int64_t max = (std::int64_t)nums[i] + t;
                auto it = s.lower_bound(min);
                if (it != s.end()) {
                    if (*it <= max) {
                        return true;
                    }
                }

                if (i - k >= 0) {
                    s.erase(nums[i - k]);
                }
            }

            s.insert(nums[i]);
        }

        return false;
    }
};

int main() {
    auto nums = std::vector {1,2,3,1};
    int k = 3, t = 0;
    bool result = Solution{}.containsNearbyAlmostDuplicate(nums, k, t);

    nums = std::vector {1,0,1,1};
    k = 1;
    t = 2;
    result = Solution{}.containsNearbyAlmostDuplicate(nums, k, t);

    nums = std::vector {1, 5, 9, 1, 5, 9};
    k = 2;
    t = 3;
    result = Solution{}.containsNearbyAlmostDuplicate(nums, k, t);

    nums = std::vector {1,6,8,4,2,5,9,2,5,8,6,7,1};
    k = 1;
    t = 1;
    result = Solution{}.containsNearbyAlmostDuplicate(nums, k, t);

    nums = std::vector {1,6,8,4,2,5,9,2,5,8,6,7,1};
    k = 1;
    t = 0;
    result = Solution{}.containsNearbyAlmostDuplicate(nums, k, t);

    nums = std::vector {1,2,2,3,4,5};
    k = 3;
    t = 0;
    result = Solution{}.containsNearbyAlmostDuplicate(nums, k, t);

    nums = std::vector{-2147483640,-2147483641};
    k = 1;
    t = 100;
    result = Solution{}.containsNearbyAlmostDuplicate(nums, k, t);

    return 0;
}
