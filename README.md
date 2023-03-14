# Giles

Simple folder watcher to see what files/folders are created, updated, deleted and renamed

For example

Offers two "modes" a default scrolling list of changes using

`GILES C:\Windows`

which gives output similar to

```
Changed: C:\Windows\Prefetch\RUNTIMEBROKER.EXE-E7A77CE5.pf
Changed: C:\Windows\Prefetch\RUNTIMEBROKER.EXE-E7A77CE5.pf
Changed: C:\Windows\Prefetch\RUNTIMEBROKER.EXE-BE455A70.pf
Changed: C:\Windows\Prefetch\RUNTIMEBROKER.EXE-BE455A70.pf
Changed: C:\Windows\System32\sru\SRUDB.dat
```

or a "counts" mode using

`GILES C:\Windows counts`

which shows the top 10 most touched files

```
Path                                                                                       C     U     D
C:\Windows\System32\Tasks\Microsoft\Windows\UpdateOrchestrator\Schedule Work               0     12    0
C:\Windows\System32\Tasks\Microsoft\Windows\UpdateOrchestrator\Schedule Scan               0     4     0
C:\Windows\Logs\WindowsUpdate                                                              0     3     0
C:\Windows\Prefetch\GIT.EXE-7A7F235A.pf                                                    0     2     0
C:\Windows\WindowsUpdate.log                                                               0     2     0
C:\Windows\System32\Tasks\Microsoft\Windows\Flighting\OneSettings\RefreshCache             0     2     0
C:\Windows\Prefetch\TASKHOSTW.EXE-D0485383.pf                                              0     2     0
C:\Windows\SoftwareDistribution\Download\Install                                           0     1     1
C:\Windows\Prefetch\MOUSOCOREWORKER.EXE-0BCC41A8.pf                                        0     2     0
C:\Windows\Prefetch\SVCHOST.EXE-7F43CD26.pf                                                0     2     0
```
