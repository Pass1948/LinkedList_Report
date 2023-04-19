using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList_Report
{
    public class LinkedListNode<T>           // 데이터 주소포인트인 노드를 구현한다
    {
        internal LinkedList<T> list;         // 어느 연결리스트에 소속되어있는지 알기 위해 리스트 초기화
        internal LinkedListNode<T> prev;     // 이전 데이터 주소와 연결된 노드
        internal LinkedListNode<T> next;     // 다음 데이터 주소와 연결된 노드
        public T Value;                      // 두 노드 사이에 값(데이터)

        // 오버로딩의 통해 해당 변수들의 값을 있을경우 없을경우를 다 고려하여 생성자 3가지를 만들어 대처한다
        public LinkedListNode(T value)
        {
            this.list = null;
            this.prev = null;
            this.next = null;
            this.Value = value;
        }

        public LinkedListNode(LinkedList<T> list, T date)
        {
            this.list = list;
            this.prev = null;
            this.next = null;
            this.Value = date;
        }

        public LinkedListNode(LinkedList<T> list, LinkedListNode<T> prev, LinkedListNode<T> next, T date)
        {
            this.list = list;
            this.prev = prev;
            this.next = next;
            this.Value = date;
        }

        public LinkedList<T> List { get { return list; } }              // 배열의 읽기 가능하게 구현 (외부에서는 읽기만 가능하게 내부에서는 수정가능케 설정)
        public LinkedListNode<T> Prev { get { return prev; } }          // 이전노드에 읽기용으로 구현
        public LinkedListNode<T> Next { get { return next; } }          // 다음노드에 읽기용으로 구현
        public T Item { get { return Value; } set { Value = value; } }    // 데이터도 내부에서만 수정가능하게 구현

    }

    public class LinkedList<T>              // LinkedList 구성
    {
        private LinkedListNode<T> head;    // 배열 처음 노드
        private LinkedListNode<T> tail;     // 배열 마지막 노드
        private int count;                  // 배열의 크기

        public LinkedList()
        {
            this.head = null;               // 헤드와 테일의 처음 생성자를 만들땐 null로 설정한다 이유는 어떤 값이 지정될지 모르기에 처음은 null로 설정해준다
            this.tail = null;
            count = 0;
        }

        public LinkedListNode<T> First { get { return head; } }
        public LinkedListNode<T> Last { get { return tail; } }
        public int Count { get { return count; } }


        // 배열 맨처음에 노드 추가==============================================================================
        public LinkedListNode<T> AddFirst(T date)  // 배열 맨처음에 노드 삽입하기
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, date);  // 함수의 매개변수를 갖는 새로운 노드를 생성함

            if (head != null)           // head가 있을 경우
            {
                newNode.next = head;    // 환형 구조가 아닌 구조에서는 헤드는 next노드밖에 연결된게 없어서 새로운 노드의 next가 헤드의 next를 갖게 대입을 한다
                head.prev = newNode;    // 새롭게 추가된 노드의 다음노드와 (전)헤드의 이전노드를 연결시킨다
            }
            else                       // head가 없을 경우
            {
                head = newNode;        // 배열에 헤드가 설정되지 않았을경우 새로추가하는 노드가 헤드가 되어야 한다
                tail = newNode;        // 테일또한 설정되어야 하는데 이유는 헤드가 없는 상황은 테일도 같이 없는 배열이기에 시작만 있고 끝이 없으면 에러가 나기에 배열의 시작과 끝을 우선 새로운 노드로 지정하고
                                       // 이후 삽입을 뒤로 진행할때 해당 노드를 테일로 지정하는 함수를 구성해야함
            }
            count++;                   // 배열 크기 늘이기
            return newNode;
        }

        // 배열 맨마지막에 노드 추가==============================================================================
        public LinkedListNode<T> AddLast(T value) // 배열 맨마지막에 새로운 노드 삽입하기
        {
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

            if (tail != null)           // tail이 있을 경우
            {
                newNode.prev = tail;    // tail은 다음노드에 연결이 없고 이전노드에 연결이 있어서 이전 tail의 연결을 갖고오도록 대입을 해준다
                tail.next = newNode;    // 새롭게 추가된 노드의 이전노드와 (전)tail의 다음노드를 연결시킨다
            }
            else                       // tail이 없을경우
            {
                head = newNode;        // 헤드와 테일을 새롭게 만들어줌
                tail = newNode;
            }
            count++;
            return newNode;
        }

        // 해당노드 앞에 새로운 노드 삽입==============================================================================
        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            // 혹시 있을 에러상황을 대처하기 위해 중간 삽입은 두가지의 예외처리를 해준다
            // 예외1: 해당 리스트에 포함된 노드가 아닌경우
            if (node.list != this)  // 다른 리스트의 것을 지우지않게 이뤄지게 예외처리를 구현
                throw new InvalidOperationException();
            // 예외 2 : 노드가 null인 경우
            if (node == null)
                throw new ArgumentNullException();

            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value); // 새로운 노드 생성

            // 중간에 삽입되는 경우 기존에 있던 노드들의 연결구조를 바꿔줘야 한다(새로운 노드와 연결하기 위해)
            newNode.next = node;                        // 해당 노드와 새로운 노드의 다음노드와 연결구성 
            newNode.prev = node.prev;                   // 해당 노드의 이전노드를 새로운 노드의 이전노드에 대입하여 기존에 이전데이터와 연결된 구성을 새로운 노드가 연결되게 된다
            node.prev = newNode;                        // 기존노드의 이전노드가 새로운 노드와 연결

            // 배열 첫번째에 삽입될 경우
            if (node.prev != null)                      // 기존노드의 이전노드가 없을경우 (head인 경우)
                node.prev.next = newNode;               // 기존노드에 이전노드가 새로운 노드의 다음노드와 연결
            else
                head = newNode;                         // 이전노드가 있지만 첫번째인경우를 생각하여 새로운 노드를 head로 한다

            count++;
            return newNode;
        }

        // 해당노드 뒤에 새로운 노드 삽입==============================================================================
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            // AddBefore함수와 같은 예외처리를 진행해 준다
            if (node.list != this)  
                throw new InvalidOperationException();
            if (node == null)
                throw new ArgumentNullException();

            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);

            newNode.prev = node;                        // 해당 노드와 새로운 노드의 이전 노드와 연결구성 
            newNode.next = node.next;                   // 해당 노드의 다음노드를 새로운 노드의 다음노드에 대입하여 기존에 다음데이터와 연결된 구성을 새로운 노드가 연결되게 된다
            node.next = newNode;                        // 기존노드의 다음노드가 새로운 노드와 연결

            // 배열 마지막에 삽입될 경우
            if (node.next != null)                      // 기존노드의 다음노드가 없을경우 (tail인 경우)
                node.next.prev = newNode;               // 기존노드에 다음노드가 새로운 노드의 이전노드와 연결
            else
                tail = newNode;                         // 다음노드가 있지만 마지막인경우를 생각하여 새로운 노드를 tail로 한다

            count++;
            return newNode;
        }

        // 노드삭제 함수 구현==============================================================================
        public void Remove(LinkedListNode<T> node)
        {
            // 다른리스트 간섭과 null일경우를 대비해서 예외처리 진행
            if (node.list != this)  
                throw new InvalidOperationException();
            if (node == null)
                throw new ArgumentNullException();

            if (head == node)               // 제거되는 노드가 head일경우
                head = node.next;           // 삭제되는 노드에 연결된 다음노드의 데이터의 노드를 head로 대입
            if (tail == node)               // 제거되는 노드가 tail일경우
                tail = node.prev;           // 삭제되는 노드에 연결된 이전 노드의 데이터의 노드를 tail로 대입

            // 삭제되는 노드의 양쪽 노드들을 연결하기 위해 연결구성 변경과정을 진행해야함
            if (node.prev != null)          // 이전 노드가 null이 아닐경우 
                node.prev.next = node.next; // 삭제되는 다음노드를 이전데이터의 다음노드로 교체
            if (node.next != null)          // 다음 노드가 null이 아닐경우 
                node.next.prev = node.prev; // 삭제되는 이전노드를 다음데이터의 이전노드로 교체
            count--;
        }

        // 삭제하는게 값인경우==============================================================================
        public bool RemoveValue(T value)
        {
            LinkedListNode<T>? findnode = FindValue(value); // 불연속적인 특성때문에 조건을 찾는 과정이 처음부터 해당데이터까지 탐색을 해야하기에 Find가 필요함
                                                            // 값을 찾는거여서 FindValue로 표기
            if (findnode != null)                           // 찾은 값이 null이 아닌경우 삭제를 진행함
            {
                Remove(findnode);       
                return true;
            }
            else
            {
                return false;
            }
        }
        // 해당 데이터(값) 찾기==============================================================================
        public LinkedListNode<T> FindValue(T value)
        {
            LinkedListNode<T> target = head;                            // 처음부터 탐색을 시작해야 하기에 타겟을 배열의 처음인 head로 설정
            EqualityComparer<T> comparer = EqualityComparer<T>.Default; // 값형식의 경우 값만 같으면 같다고 생각된다, 참조형식인경우 주소값만 같으면 같다고 함

            while (target != null)                                      // 찾을때 까지 탐색을 진행해야 해서 while반복문으로 진행
            {
                if (comparer.Equals(value, target.Value))               // 타겟의 값과 해당데이터의 값비교
                    return target;                                      // 찾을경우 타겟 반환
                else
                    target = target.next;                               // 아닐경우 다음으로 이동
            }

            return null;
        }

        // 삭제하는게 노드인경우==============================================================================
        public bool RemoveNode(LinkedListNode<T> node)
        {
            LinkedListNode<T>? findnode = FindNode(node);               // 노드용 Find를 사용
            if (findnode != null)
            {
                Remove(findnode);
                return true;
            }
            else
            {
                return false;
            }
        }
        // 해당 노드 찾기
        public LinkedListNode<T> FindNode(LinkedListNode<T> node)      // 값용 find를 노드제거 함수에 사용할수 없어서 별도로 구현
        {
            LinkedListNode<T> target = head;                           // 메커니즘은 동일하다

            while (target != null)         
            {
                if (node == target)                                     // 타겟도 노드여서 해당 노드와 비교하는 간단한 식으로 가능함
                    return target;
                else
                    target = target.next;
            }

            return null;
        }
    }
}
