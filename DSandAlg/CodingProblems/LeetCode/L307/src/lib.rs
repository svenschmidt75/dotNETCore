// 307. Range Sum Query - Mutable
// https://leetcode.com/problems/range-sum-query-mutable/


// SS: alternatively, could use a Fenwick tree
struct NumArray {
    nums: Vec<i32>,
    prefix_sum: Vec<i32>,
}

/**
 * `&self` means the method takes an immutable reference
 * If you need a mutable reference, change it to `&mut self` instead
 */
impl NumArray {
    fn new(nums: Vec<i32>) -> Self {
        let mut ps = vec![];
        ps.push(0);

        for i in 0..nums.len() {
            let v = ps[i] + nums[i];
            ps.push(v);
        }

        Self {
            nums,
            prefix_sum: ps,
        }
    }

    fn update(&mut self, index: i32, val: i32) {
        let delta = val - self.nums[index as usize];
        for i in (index as usize + 1)..self.prefix_sum.len() {
            self.prefix_sum[i] += delta;
        }
        self.nums[index as usize] = val;
    }

    fn sum_range(&self, left: i32, right: i32) -> i32 {
        let sum = self.prefix_sum[right as usize + 1] - self.prefix_sum[left as usize];
        sum
    }
}

/**
 * Your NumArray object will be instantiated and called as such:
 * let obj = NumArray::new(nums);
 * obj.update(index, val);
 * let ret_2: i32 = obj.sum_range(left, right);
 */
#[cfg(test)]
mod tests {
    use crate::NumArray;

    #[test]
    fn test1() {
        // arrange
        let mut ps = NumArray::new(vec![1, 3, 5]);

        // act
        // assert
        assert_eq!(ps.sum_range(0, 2), 9);
        ps.update(1, 2);
        assert_eq!(ps.sum_range(0, 2), 8);
    }

    #[test]
    fn test2() {
        // arrange
        let mut ps = NumArray::new(vec![3]);

        // act
        // assert
        assert_eq!(ps.sum_range(0, 0), 3);
        ps.update(0, 2);
        assert_eq!(ps.sum_range(0, 0), 2);
    }

    #[test]
    fn test3() {
        // arrange
        let mut ps = NumArray::new(vec![1, 5, 9, 13, 2, 7]);

        // act
        // assert
        assert_eq!(ps.sum_range(1, 3), 27);
        ps.update(2, 11);
        assert_eq!(ps.sum_range(1, 3), 29);
    }

}
