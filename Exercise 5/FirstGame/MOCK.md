# MOCK
- Some lib
    - java.util.*
    - java.lang.*
    - java.io.*
- First mock
    - self-introduction
    - duplicate cases caution
    - 
- Second mock
    - Collection**s**.sort() -> (objects sort), Array**s**.sort() -> sort primitive types. **BOTH OF THEM HAVE A 'S' IN THE END**
    - mlgm + nlgn can be better than (m+n)lg(m+n), seperate sort can be better than sort them together.
    - Something more:
        - if sort set, we can put them in treeset, or use this code:
```java
Set yourHashSet = new HashSet();
List sortedList = new ArrayList(yourHashSet);
Collections.sort(sortedList);

String domains[] = {"Practice", "Geeks", "Code", "Quiz"}; 
// Here we are making a list named as Collist 
List colList = new ArrayList(Arrays.asList(domains)); 
// Remember Arrays.asList()
```

# Some terms
- Permutation
- loop over
- big O notation -> O of N
- x squared $x^2$, x cubed $x^3$, x to the power of y $(x^y)$
- lexographic
- 

# Facebook tips
1. Ask question to clarify the problem;
2. Explain your approach before writing code;
3. Take time to check code with an example -> to find potential bug;
4. TEST by checking edge cases[**YOU NEED TO TELL INTERVIEWER THAT YOU ARE GOING TO TEST YOU CODE BY SELF**];
5. Analyze time and space complexity;
6. Ask question to interviewer.
    1. What is a typical day like?
    2. Why you come to work here?
    3. What is your most excited exprience in facebook?
    4. Could you give me a specific example of facebook culture? like culture of 
   ```
   Be bold
   Focus on impact
   Move fast
   Be open
   Build social value
   ```