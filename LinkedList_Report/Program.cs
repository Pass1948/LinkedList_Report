﻿namespace LinkedList_Report
{
    internal class Program
    {
        /*===============================기술 면접 LinkedList정리===============================
         * <LinkedList>
         * LinkedList(연결리스트)는 힙영역에 데이터들이 불연속적으로 개별 할당되어 진다
         * 데이터들은 각자 해당 데이터가 다음 데이터를 주소 참조하는 방식으로 서로 연결되었있는데
         * 불연속적이기에 인덱스라는 개념이 없고 대신 데이터의 주소를 참조하는 주소포인트인 
         * 노드(node)를 가지게 되는데 이 노드는 클래스로 구현되고 데이터와 구성은
         * 이전 데이터를 참조하는 노드+데이터+ 다음 데이터를 참조하는 노드 이런 샌드위치식 구성으로 힙영역에 할당되어진다.
         * 이러한 구조를 갖고있어서 기존에 배열과 리스트 보단 추가와 삭제가 자유로운 편인데 불연속적이다 보니 중간에 삽입/삭제가 가능한대 이 경우
         * 새로운 데이터가 기존에 데이터들 사이에 노드로 참조된 연결구성을 재구성 하는 방식으로 삽입/삭제가 이뤄진다.
         * 삽입/삭제가 순서에 상관없이 자유롭게 이뤄지다 보니 한번의 연산으로 효율적인 삽입/삭제가 가능하게 되어 LinkedList의 시간복잡도에서 삽입과 삭제
         * 부분은 O(1) 표기되어 지는데 반면 접근과 탐색은 LinkedList가 불연속적이다 보니 위치를 한번에 특정짓기에 어려움이 있는데
         * 처음 데이터 노드를 LinkedList에서는 head라고 부르며 끝 데이터노드를 tail이라고 부르는데 과정(접근, 탐색)을 진행하게 되면
         * head 부터 tail까지 각 노드의 연결구성을 n번 만큼 걸처야 한다 그래서 시간복잡도의 접근과 탐색 부분에서 O(n)로 표기된다
         */

        static void Main(string[] args)
        {
            LinkedList_Report.LinkedList<int> linkedList = new LinkedList_Report.LinkedList<int>();

            linkedList.AddFirst(0);
            linkedList.AddFirst(1);
            linkedList.AddFirst(2);
            linkedList.AddFirst(3);
            linkedList.AddFirst(4);
        }
    }
}