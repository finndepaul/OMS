# Cách chạy dự án:
- B1: Set as start-up project OMS.API
- B2: Mở Package Manager Console > Ở Default project chọn OMS.Infrastructure
- B3: Vào folder Database > Mở folder AppDbContexts > Sửa Connection string ở 2 file là OMSReadOnlyDbContext và OMSReadWriteDbContext
- B4: Sửa Connection string ở ở appsetting.json ở OMS.API
- B5: Ở cửa sổ Package Manager Console > gõ các câu lệnh:
-> add-migration [đặt tên cho migration] -context OMSReadOnlyDbContext
-> add-migration [đặt tên cho migration] -context OMSReadWriteDbContext
-> update-database [tên migration của db OMSReadWriteDbContext] -context OMSReadWriteDbContext
# ERD:
- https://dbdiagram.io/d/OMS-687d1510f413ba3508c02b3f
