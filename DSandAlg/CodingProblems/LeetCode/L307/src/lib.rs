// 307. Range Sum Query - Mutable
// https://leetcode.com/problems/range-sum-query-mutable/

struct NumArray {
    nums: Vec<i32>,
    fenwick: Vec<i32>,
}

/**
 * `&self` means the method takes an immutable reference
 * If you need a mutable reference, change it to `&mut self` instead
 */
impl NumArray {
    fn new(nums: Vec<i32>) -> Self {
        let mut ps = vec![0; nums.len() + 1];

        for i in 0..nums.len() {
            let mut idx = i + 1;
            while idx <= nums.len() {
                ps[idx] += nums[i];
                idx += NumArray::lsb(idx as i32) as usize;
            }
        }

        Self {
            nums,
            fenwick: ps,
        }
    }

    fn lsb(v: i32) -> i32 {
        v & -v
    }

    fn update(&mut self, index: i32, val: i32) {
        let delta = val - self.nums[index as usize];

        let mut j = index as usize + 1;
        while j < self.fenwick.len() {
            self.fenwick[j] += delta;
            j += NumArray::lsb(j as i32) as usize;
        }

        self.nums[index as usize] = val;
    }

    fn get_sum(&self, idx: i32) -> i32 {
        let mut sum = 0;
        let mut j = idx as usize;
        while j > 0 {
            sum += self.fenwick[j];
            j -= NumArray::lsb(j as i32) as usize;
        }

        sum
    }

    fn sum_range(&self, left: i32, right: i32) -> i32 {
        let sum1 = self.get_sum(left);
        let sum2 = self.get_sum(right + 1);
        sum2 - sum1
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
