# OSDB

Unity로 제작한 2D RPG 클라이언트 프로젝트입니다. 로그인, 서버 선택, 캐릭터 조회/생성, 캐릭터 스탯 표시까지 이어지는 온라인 게임의 기본 로비 플로우를 구현했습니다.

이 프로젝트는 단순한 화면 전환 예제가 아니라, Unity UI와 서버 통신, 캐릭터 데이터 표시, SQLite 접근 구조를 함께 다루며 게임 클라이언트가 백엔드 데이터와 연결되는 과정을 학습하고 구현한 결과물입니다.

## 프로젝트 개요

OSDB는 사용자가 계정으로 로그인한 뒤 서버를 선택하고, 서버에 저장된 캐릭터 목록을 확인하거나 새 캐릭터를 생성할 수 있는 2D RPG 클라이언트입니다. 선택한 캐릭터는 레벨, 직업, STR, INT, DEX, LUK 등의 스탯 정보가 UI에 표시됩니다.

주요 흐름은 다음과 같습니다.

1. 로그인 화면에서 ID와 비밀번호 입력
2. PHP 서버 API를 통해 로그인 검증
3. 서버 선택 후 서버 인구 정보 조회
4. 해당 서버의 캐릭터 목록 표시
5. 캐릭터 선택 시 상세 스탯 표시
6. 직업과 닉네임을 입력해 캐릭터 생성
7. 게임 시작 시 메인 씬으로 이동

## 주요 기능

- 로그인 UI 및 인증 결과 처리
- 서버 선택 및 서버 정보 표시
- 보유 캐릭터 목록 동적 렌더링
- 캐릭터 직업별 이미지 매핑
- 캐릭터 상세 스탯 UI 갱신
- 닉네임 중복 확인 후 캐릭터 생성
- 팝업 기반 사용자 피드백 처리
- UnityWebRequest 기반 PHP 서버 통신
- SQLite 접근을 위한 공통 데이터베이스 클래스 구성
- Lobby, Main, House, Clear 씬 기반 게임 진행 구조

## 기술 스택

| 분류 | 사용 기술 |
| --- | --- |
| Engine | Unity 2022.3.27f1 |
| Language | C# |
| UI | Unity UGUI |
| Network | UnityWebRequest, WWWForm |
| Database | SQLite, Mono.Data.Sqlite |
| External API | PHP endpoint 연동 구조 |
| Assets | SPUM, Pixel Heroes Pack, Rogue Adventure tiles |

## 구현 포인트

### 1. 로그인과 UI 상태 관리

`GameManager`는 로그인, 서버 선택, 캐릭터 선택, 캐릭터 생성 UI를 중앙에서 관리합니다. 로그인 성공 여부에 따라 화면을 전환하고, 팝업 UI를 통해 생성 성공/실패, 닉네임 중복 등의 상태를 사용자에게 보여줍니다.

관련 파일:

- `OSDB/Assets/Scripts/GameManager.cs`
- `OSDB/Assets/Scripts/PHP/PHPLogIn.cs`

### 2. 서버 및 캐릭터 선택 흐름

`ServerManager`는 서버 버튼 선택, 서버 인구 정보 표시, 캐릭터 목록 표시, 캐릭터 스탯 표시를 담당합니다. 서버 선택 후 PHP API 응답을 파싱해 UI에 반영하며, 사용자가 선택한 캐릭터의 상세 정보를 별도 패널에 출력합니다.

관련 파일:

- `OSDB/Assets/Scripts/ServerManager.cs`
- `OSDB/Assets/Scripts/PHP/PHPSelectServer.cs`
- `OSDB/Assets/Scripts/PHP/PHPSelectCharacter.cs`
- `OSDB/Assets/Scripts/PHP/PHPGetServerInfo.cs`

### 3. 캐릭터 생성과 닉네임 중복 검사

캐릭터 생성은 직업 선택, 생성 확인 팝업, 닉네임 입력, 중복 검사, 생성 요청 순서로 진행됩니다. `PHPCreateCharacter`는 닉네임 중복 검사를 먼저 수행하고, 중복이 없을 때만 캐릭터 생성 API를 호출합니다.

관련 파일:

- `OSDB/Assets/Scripts/PHP/PHPCreateCharacter.cs`

### 4. 직업별 캐릭터 이미지 매핑

`CharacterSlotManager`는 캐릭터 직업명에 따라 슬롯 이미지를 교체합니다. 서버에서 받은 캐릭터 데이터와 Unity UI 슬롯을 연결해 캐릭터 선택 화면을 구성합니다.

관련 파일:

- `OSDB/Assets/Scripts/CharacterSlotManager.cs`

### 5. SQLite 접근 구조

`DatabaseAccess`와 `DBController`는 SQLite 연결, 쿼리 실행, 테이블 조회, 삽입, 수정, 삭제를 수행하는 구조를 제공합니다. 프로젝트에는 PHP API 연동 흐름과 함께 로컬 데이터베이스 접근 실험 코드도 포함되어 있습니다.

관련 파일:

- `OSDB/Assets/DatabaseAccess.cs`
- `OSDB/Assets/Scripts/DBController.cs`

## 프로젝트 구조

```text
OSDB/
  Assets/
    Scenes/
      Lobby.unity
      Main.unity
      House.unity
      Clear.unity
    Scripts/
      GameManager.cs
      ServerManager.cs
      CharacterSlotManager.cs
      DBController.cs
      PHP/
        PHPLogIn.cs
        PHPSelectServer.cs
        PHPSelectCharacter.cs
        PHPCreateCharacter.cs
        PHPGetServerInfo.cs
    Resources/
    Prefabs/
    Plugins/
  Packages/
    manifest.json
  ProjectSettings/
```

## 실행 방법

1. Unity Hub에서 `OSDB` 폴더를 프로젝트로 엽니다.
2. Unity Editor 버전은 `2022.3.27f1`을 권장합니다.
3. `Assets/Scenes/Lobby.unity` 씬을 엽니다.
4. Play 버튼을 눌러 로그인 화면부터 실행합니다.

PHP API 연동을 테스트하려면 로컬 서버에 다음 엔드포인트가 준비되어 있어야 합니다.

- `http://localhost/Login.php`
- `http://localhost/SelectServer.php`
- `http://localhost/SelectCharacter.php`
- `http://localhost/CheckDuplicate.php`
- `http://localhost/CreateCharacter.php`
- `http://localhost/GetServerInfo.php`

## 씬 구성

| 씬 | 역할 |
| --- | --- |
| `Lobby` | 로그인, 서버 선택, 캐릭터 선택/생성 UI |
| `Main` | 게임 메인 플레이 씬 |
| `House` | 추가 게임 공간 씬 |
| `Clear` | 클리어 화면 또는 결과 씬 |

## 배운 점

- Unity UI 오브젝트를 여러 상태로 분리하고 화면 흐름을 관리하는 방법
- `UnityWebRequest`와 `WWWForm`을 사용해 서버에 데이터를 전송하는 방법
- 서버 응답 문자열을 파싱해 캐릭터 목록과 스탯 UI에 반영하는 방법
- Singleton 패턴을 사용해 매니저 객체 간 상태를 공유하는 방법
- SQLite 연결 클래스를 구성하고 Unity 프로젝트에서 데이터베이스 접근을 실험하는 방법

## 개선하고 싶은 부분

- 서버 응답 포맷을 문자열 파싱 방식에서 JSON 기반 구조로 변경
- 로그인 및 DB 쿼리 처리에서 보안성 강화
- API URL을 코드에 직접 두지 않고 설정 파일 또는 ScriptableObject로 분리
- 캐릭터/서버 데이터 모델 클래스를 도입해 파싱 로직 정리
- Unity 로그 및 사용자 설정 파일을 Git 추적 대상에서 제외

## 한 줄 소개

Unity와 서버 통신을 연결해 온라인 RPG의 로그인-서버 선택-캐릭터 관리 로비 흐름을 구현한 2D 게임 클라이언트 프로젝트입니다.
