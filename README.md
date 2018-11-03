# SeqDistK
+ A tools to calculate the distance among sequence. Contians Eu, Ma, Ch, d2, d2star, d2S, Hao. 
+ Support Windows. 
+ Convenience. The whole process can be operated by mouse.

## Installation 
+ Install .Net Framework 4.5.2 
+ Download SeqDistK.exe from realease
+ Double click SeqDistK.exe

## Runing

### N to N mode
<img src="https://github.com/htczero/SeqDistK/blob/master/img/NtoN.png" />
+ Group by directory. If there are N sequences in the directory, you will get N by N matrix
+ Support multiple directory and multilevel directory structure. Each directory will calculate independently. The result will save in save directory structure.
+ If you have select d2star or d2S, you must select Markove value(M)
<img src="https://github.com/htczero/SeqDistK/blob/master/img/markov.png" />

### 1 to N mode
<img src="https://github.com/htczero/SeqDistK/blob/master/img/1toN.png" />
+ Input structure unit must be 

  directory-unit
      single-seq.txt
	  directory-multi-seqs
	      seq1.txt
		  seq2.txt
		  ...
		  seqN.txt
		
  

  then, you will get one by N matrix
+ Support multiple directory-unit and multilevel directory structure. Each directory-unit will calculate independently. The result will save in save directory structure.

